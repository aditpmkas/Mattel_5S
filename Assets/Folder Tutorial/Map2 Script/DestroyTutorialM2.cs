using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTurorialM2 : MonoBehaviour
{
    public AudioSource audioTrash;
    public AudioClip clipTrash;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sort"))
        {
            PlayTrashSound();
            Destroy(other.gameObject);
            FindObjectOfType<SortingTutorialM2>()?.IncrementCorrectSort();
        }
        else if (other.CompareTag("Unsort"))
        {
            PlayTrashSound();
            Destroy(other.gameObject);
            // Tambahkan feedback atau efek penalti jika ingin
        }
    }

    private void PlayTrashSound()
    {
        if (audioTrash != null && clipTrash != null)
        {
            audioTrash.PlayOneShot(clipTrash);
        }
    }
}
