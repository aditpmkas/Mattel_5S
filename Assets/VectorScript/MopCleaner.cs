using UnityEngine;
using UnityEngine.SceneManagement;

public class MopCleaner : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DirtyFloor"))
        {
            Destroy(other.gameObject); // Hapus objek kotoran
            Debug.Log("Pel membersihkan: " + other.name);
        }
    }
}
