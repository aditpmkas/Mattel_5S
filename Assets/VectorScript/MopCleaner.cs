using UnityEngine;

public class MopCleaner : MonoBehaviour
{
    //[SerializeField] private AudioClip cleanSound;
    //private AudioSource audioSource;

    private void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DirtyFloor"))
        {
            Destroy(other.gameObject); // Hilangkan kotoran
        }
    }
}
