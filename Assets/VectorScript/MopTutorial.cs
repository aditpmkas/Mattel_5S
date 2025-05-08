using UnityEngine;
using UnityEngine.SceneManagement;

public class MopTutorial : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DirtyFloor"))
        {
            ShineTutorial.Instance.DirtCleaned(); // Beri tahu bahwa 1 kotoran dibersihkan
            Destroy(other.gameObject); // Hapus objek kotoran
            Debug.Log("Pel membersihkan: " + other.name);
        }
    }
}
