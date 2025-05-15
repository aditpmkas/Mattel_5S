using UnityEngine;

public class MopLevel1Map1 : MonoBehaviour
{
    private int cleanedCount = 0;
    public int requiredCleanCount = 3;

    [Header("Cleaning Settings")]
    public float requiredSwipeDistance = 0.2f;

    [Header("UI")]
    public GameObject successPanel;

    [Header("Objective Tracker")]
    public ShineChecker shineChecker; // Diisi dari Inspector

    private Collider currentDirty;
    private Vector3 lastPosition;
    private float sweptDistance;

    private void Start()
    {
        if (successPanel != null)
            successPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DirtyFloor"))
        {
            currentDirty = other;
            lastPosition = transform.position;
            sweptDistance = 0f;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other == currentDirty)
        {
            Vector3 delta = transform.position - lastPosition;
            sweptDistance += delta.magnitude;
            lastPosition = transform.position;

            if (sweptDistance >= requiredSwipeDistance)
            {
                // Clean the dirt
                Destroy(currentDirty.gameObject);
                cleanedCount++;
                Debug.Log($"Pel membersihkan: {currentDirty.name} | Total: {cleanedCount}");

                if (shineChecker != null)
                    shineChecker.RegisterShineHit();
                else
                    Debug.LogWarning("ShineChecker belum di-assign ke MopLevel1Map1!");

                if (cleanedCount >= requiredCleanCount && successPanel != null)
                {
                    successPanel.SetActive(true);
                    Debug.Log("Semua kotoran sudah dibersihkan!");
                }

                currentDirty = null;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == currentDirty)
        {
            // reset jika mop pergi sebelum cukup jauh
            currentDirty = null;
        }
    }
}
