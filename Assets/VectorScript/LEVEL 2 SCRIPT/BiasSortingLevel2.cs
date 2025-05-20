using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiasSortingLevel2 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SortBias"))
        {
            ProgressManagerLevel2.Instance.sortedBiasItemsCount++;
            Debug.Log("SortBias item sorted. Total: " + ProgressManagerLevel2.Instance.sortedBiasItemsCount);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SortBias"))
        {
            ProgressManagerLevel2.Instance.sortedBiasItemsCount--;
            Debug.Log("SortBias item removed. Total: " + ProgressManagerLevel2.Instance.sortedBiasItemsCount);
        }
    }
}
