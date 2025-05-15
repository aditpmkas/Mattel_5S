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
            bool sortingDone = ProgressManagerLevel2.Instance.sortingDone;
            bool setInOrderDone = ProgressManagerLevel2.Instance.setInOrderDone;

            int scoreToAdd = (sortingDone && setInOrderDone) ? 100 : 100 - 25;

            shineScore += scoreToAdd;
            ProgressManagerLevel2.Instance.AddScore(scoreToAdd);

            ProgressManagerLevel2.Instance.cleanedPuddlesCount++;
            if (ProgressManagerLevel2.Instance.cleanedPuddlesCount >= ProgressManagerLevel2.Instance.totalPuddlesCount)
            {
                ProgressManagerLevel2.Instance.shineDone = true;
                Debug.Log("Shine task complete!");
            }

            Destroy(other.gameObject);
        }
    }
}
