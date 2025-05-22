using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShineTrashSortingLevel2Map2 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RootCauseLevel2"))
        {
            int pointsToAdd = 100;

            if (!ProgressManagerLevel2Map2.Instance.sortingDone || !ProgressManagerLevel2Map2.Instance.setInOrderDone)
            {
                pointsToAdd -= 25;
                Debug.Log("Penalty applied: Sorting or SetInOrder not complete (-25)");
            }

            ProgressManagerLevel2Map2.Instance.AddScore(pointsToAdd);
            ProgressManagerLevel2Map2.Instance.cleanedRootCauseCount++;

            Debug.Log("Root Cause trash sorted: " + ProgressManagerLevel2Map2.Instance.cleanedRootCauseCount + " / " + ProgressManagerLevel2Map2.Instance.totalRootCauseCount);

            // Check if shine is done
            ProgressManagerLevel2Map2.Instance.CheckShineTaskComplete();

            // Optionally destroy object after sorting
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RootCauseLevel2"))
        {
            // If you don't destroy on enter, handle removing it here:
            ProgressManagerLevel2Map2.Instance.cleanedRootCauseCount--;
            ProgressManagerLevel2Map2.Instance.SubtractScore(100); // Or whatever score logic you prefer
            Debug.Log("Root Cause trash removed. Count: " + ProgressManagerLevel2Map2.Instance.cleanedRootCauseCount);
        }
    }
}
