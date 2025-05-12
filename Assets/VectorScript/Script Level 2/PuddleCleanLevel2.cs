using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuddleCleanLevel2 : MonoBehaviour
{
    public int shineScore = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "DirtyFloor")
        {
            int scoreToAdd = (!ProgressManagerLevel2.Instance.sortingDone) ? 100 - 25 : 100;

            shineScore += scoreToAdd;
            ProgressManagerLevel2.Instance.AddScore(scoreToAdd);

            Destroy(other.gameObject);
            Debug.Log("GameObject " + other.gameObject.name + " destroyed.");
            Debug.Log("Your score is: " + shineScore);
        }
    }
}
