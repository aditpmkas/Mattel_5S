using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuddleCleanLevel2 : MonoBehaviour
{
    public int shineScore = 0;
    public AudioSource wipingSFX;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("DirtyFloor") || other.CompareTag("DirtyFloor"))
        {
            // Get puddle health
            PuddleHealthLevel2 puddleHealth = other.GetComponent<PuddleHealthLevel2>();

            if (puddleHealth != null)
            {
                puddleHealth.AddSwipe();

                // Play mop swipe sfx here
                if (wipingSFX != null)
                {
                    wipingSFX.Play();
                }

                // Only score when puddle destroyed
                if (puddleHealth.IsDestroyed())
                {
                    bool sortingDone = ProgressManagerLevel2.Instance.sortingDone;
                    bool setInOrderDone = ProgressManagerLevel2.Instance.setInOrderDone;

                    int scoreToAdd = (sortingDone && setInOrderDone) ? 100 : 100 - 25;

                    // Apply -10 penalty if not all cracks fixed yet
                    if (ProgressManagerLevel2.Instance.fixedCracksCount < ProgressManagerLevel2.Instance.totalCracksCount)
                    {
                        scoreToAdd -= 10;
                        Debug.Log("Penalty applied: Crack not fixed before mopping (-10)");
                    }

                    shineScore += scoreToAdd;
                    ProgressManagerLevel2.Instance.AddScore(scoreToAdd);

                    ProgressManagerLevel2.Instance.cleanedPuddlesCount++;
                    if (ProgressManagerLevel2.Instance.cleanedPuddlesCount >= ProgressManagerLevel2.Instance.totalPuddlesCount)
                    {
                        ProgressManagerLevel2.Instance.shineDone = true;
                        Debug.Log("Shine task complete!");
                    }
                }
            }
        }
    }
}
