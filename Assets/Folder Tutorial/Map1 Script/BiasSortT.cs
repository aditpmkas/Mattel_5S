using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiasSortT : MonoBehaviour
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
        if (other.CompareTag("SortBias"))
        {
            sorter?.IncrementBiasCount();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SortBias"))
        {
            sorter?.DecrementBiasCount();
        }
    }
}
