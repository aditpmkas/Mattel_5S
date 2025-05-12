using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSortingLevel2 : MonoBehaviour
{
    public int sortScore = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sort")
        {
            sortScore += 100;
            ProgressManagerLevel2.Instance.AddScore(100);
            Destroy(other.gameObject);
            Debug.Log("GameObject " + other.gameObject.name + " destroyed.");
            Debug.Log("Your score is: " + sortScore);

            // Mark Sorting as done
            ProgressManagerLevel2.Instance.sortingDone = true;
        }

        if (other.gameObject.tag == "Unsort")
        {
            sortScore -= 50;
            ProgressManagerLevel2.Instance.SubtractScore(50);
            Destroy(other.gameObject);
            Debug.Log("GameObject " + other.gameObject.name + " destroyed.");
            Debug.Log("Your score is: " + sortScore);
        }
    }
}
