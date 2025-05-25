using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTurorial : MonoBehaviour
{
    private SortingTutorial sorter;

    [Header("Trash Sound")]
    public AudioSource audioTrash;
    public AudioClip clipTrash;
    private void Awake()
    {
        sorter = FindObjectOfType<SortingTutorial>();
        if (sorter == null)
            Debug.LogError("SortingTutorial not found in scene!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sort"))
        {
            // Hancurkan objek Sort dan hitung
            Destroy(other.gameObject);
            sorter.IncrementCorrectSort();
            PlaySound();
        }
        else if (other.CompareTag("Unsort"))
        {
            // Hancurkan objek Unsort, tapi tidak menghitung
            Destroy(other.gameObject);
            PlaySound();
        }
    }

    private void PlaySound()
    {
        if (audioTrash != null && clipTrash != null)
        {
            audioTrash.PlayOneShot(clipTrash);
        }
    }
}
