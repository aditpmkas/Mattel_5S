using UnityEngine;
using UnityEngine.SceneManagement;

public class MopTutorial : MonoBehaviour
{
    [Header("Cleaning Settings")]
    [Tooltip("Minimal jarak sweep (dalam meter) di dalam kotoran sebelum dianggap bersih")]
    public float requiredSwipeDistance = 0.2f;

    private Collider currentDirty;    // kotoran yang sedang kita hitung
    private Vector3 lastPosition;     // posisi mop terakhir frame lalu
    private float sweptDistance;      // akumulasi jarak sapuan

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DirtyFloor"))
        {
            // mulai menghitung sweep saat pertama kali menyentuh kotoran
            currentDirty = other;
            lastPosition = transform.position;
            sweptDistance = 0f;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other == currentDirty)
        {
            // hitung perpindahan mop sejak frame sebelumnya
            Vector3 delta = transform.position - lastPosition;
            sweptDistance += delta.magnitude;
            lastPosition = transform.position;

            // kalau sudah di-sweep cukup jauh, bersihkan
            if (sweptDistance >= requiredSwipeDistance)
            {
                ShineTutorial.Instance.DirtCleaned(); // notify
                Destroy(currentDirty.gameObject);     // hapus kotoran
                currentDirty = null;                  // reset state
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == currentDirty)
        {
            // kalau mop dilepas sebelum jarak tercapai, batalkan
            currentDirty = null;
        }
    }
}
