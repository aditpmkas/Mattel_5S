using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuddleCleanLevel2Map2 : MonoBehaviour
{
    public int shineScore = 0;
    public AudioSource wipingSFX;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("DirtyFloor") || other.CompareTag("DirtyFloor"))
        {
            PuddleHealthLevel2 puddleHealth = other.GetComponent<PuddleHealthLevel2>();

            if (puddleHealth != null)
            {
                puddleHealth.AddSwipe();

                if (wipingSFX != null)
                {
                    wipingSFX.Play();
                }

                if (puddleHealth.IsDestroyed())
                {
                    bool sortingDone = ProgressManagerLevel2Map2.Instance.sortingDone;
                    bool setInOrderDone = ProgressManagerLevel2Map2.Instance.setInOrderDone;

                    int scoreToAdd = (sortingDone && setInOrderDone) ? 100 : 100 - 25;

                    // Apply -10 penalty if cracks or root causes aren't fully done yet
                    if (ProgressManagerLevel2Map2.Instance.fixedCracksCount < ProgressManagerLevel2Map2.Instance.totalCracksCount
                        || ProgressManagerLevel2Map2.Instance.cleanedRootCauseCount < ProgressManagerLevel2Map2.Instance.totalRootCauseCount)
                    {
                        scoreToAdd -= 10;
                        Debug.Log("Penalty applied: Crack/RootCause not done before mopping (-10)");
                    }

                    shineScore += scoreToAdd;
                    ProgressManagerLevel2Map2.Instance.AddScore(scoreToAdd);

                    ProgressManagerLevel2Map2.Instance.cleanedPuddlesCount++;
                    Debug.Log("Puddles cleaned: " + ProgressManagerLevel2Map2.Instance.cleanedPuddlesCount + " / " + ProgressManagerLevel2Map2.Instance.totalPuddlesCount);

                    ProgressManagerLevel2Map2.Instance.CheckShineTaskComplete();
                }
            }
        }
    }
}
