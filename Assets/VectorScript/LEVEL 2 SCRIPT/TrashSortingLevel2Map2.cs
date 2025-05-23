using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSortingLevel2Map2 : MonoBehaviour
{
    public int sortScore = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sort")
        {
            sortScore += 100;
            ProgressManagerLevel2Map2.Instance.AddScore(100);
            ProgressManagerLevel2Map2.Instance.sortedTrashCount++;
            if (ProgressManagerLevel2Map2.Instance.sortedTrashCount >= ProgressManagerLevel2Map2.Instance.totalTrashCount)
            {
                ProgressManagerLevel2Map2.Instance.sortingDone = true;
                Debug.Log("Sorting task complete!");
            }

            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Unsort")
        {
            sortScore -= 50;
            ProgressManagerLevel2Map2.Instance.SubtractScore(50);
            Destroy(other.gameObject);
            Debug.Log("GameObject " + other.gameObject.name + " destroyed.");
            Debug.Log("Your score is: " + sortScore);
        }

        if (other.gameObject.tag == "SortBias")
        {
            ProgressManagerLevel2Map2.Instance.SubtractScore(25);
            Destroy(other.gameObject);
            Debug.Log("GameObject " + other.gameObject.name + " destroyed.");
        }
    }
}
