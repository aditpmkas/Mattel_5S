using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackFixButtonHandlerLevel2 : MonoBehaviour
{
    public GameObject canvasObject;
    public AudioSource clickSFX; // Add this new field in Inspector

    public void FixCrackAndDisableCanvasObject()
    {
        int pointsToAdd = 100;

        // Apply 25 points penalty if Sorting or SetInOrder is not yet complete
        if (!ProgressManagerLevel2.Instance.sortingDone || !ProgressManagerLevel2.Instance.setInOrderDone)
        {
            pointsToAdd -= 25;
            Debug.Log("Penalty applied: Sorting or SetInOrder not complete (-25)");
        }

        // Add score
        ProgressManagerLevel2.Instance.AddScore(pointsToAdd);
        Debug.Log("Crack fix score added: " + pointsToAdd);

        // Add crack progress
        ProgressManagerLevel2.Instance.AddCrackFix();

        // Disable this crack's canvas
        if (canvasObject != null)
        {
            canvasObject.SetActive(false);
        }

        // Disable this button GameObject itself
        gameObject.SetActive(false);

        // Play button click SFX
        if (clickSFX != null)
        {
            clickSFX.Play();
        }
    }

}
