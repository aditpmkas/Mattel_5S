using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiasSortingLevel2Map2 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SortBias"))
        {
            ProgressManagerLevel2Map2.Instance.sortedBiasItemsCount++;
            Debug.Log("SortBias item sorted. Total: " + ProgressManagerLevel2Map2.Instance.sortedBiasItemsCount);
        }

        if (other.CompareTag("Sort"))
        {
            ProgressManagerLevel2Map2.Instance.sortedTrashCount++;
            ProgressManagerLevel2Map2.Instance.AddScore(50);
            Debug.Log("Sort item sorted. +50 points awarded!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SortBias"))
        {
            ProgressManagerLevel2Map2.Instance.sortedBiasItemsCount--;
            Debug.Log("SortBias item removed. Total: " + ProgressManagerLevel2Map2.Instance.sortedBiasItemsCount);
        }

        if (other.CompareTag("Sort"))
        {
            ProgressManagerLevel2Map2.Instance.sortedTrashCount--;
            ProgressManagerLevel2Map2.Instance.SubtractScore(50);
            Debug.Log("Sort item removed. -50 points deducted!");
        }
    }
}
