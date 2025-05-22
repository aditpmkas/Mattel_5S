using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiasSortM2 : MonoBehaviour
{
    private SortingTutorialM2 sorter;

    private void Awake()
    {
        sorter = FindObjectOfType<SortingTutorialM2>();
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
