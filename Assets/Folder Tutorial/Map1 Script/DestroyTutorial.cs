using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTurorial : MonoBehaviour
{
    private SortingTutorial sorter;

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
            sorter?.IncrementCorrectSort();
        }
        else if (other.CompareTag("Unsort"))
        {
            // Hancurkan objek Unsort, tapi tidak menghitung
            Destroy(other.gameObject);
        }
    }
}
