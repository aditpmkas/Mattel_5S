using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackFixButtonHandlerLevel2Map2 : MonoBehaviour
{
    public GameObject canvasObject;

    public void FixCrackAndDisableCanvasObject()
    {
        int pointsToAdd = 100;

        if (!ProgressManagerLevel2Map2.Instance.sortingDone || !ProgressManagerLevel2Map2.Instance.setInOrderDone)
        {
            pointsToAdd -= 25;
            Debug.Log("Penalty applied: Sorting or SetInOrder not complete (-25)");
        }

        ProgressManagerLevel2Map2.Instance.AddScore(pointsToAdd);
        Debug.Log("Crack fix score added: " + pointsToAdd);

        ProgressManagerLevel2Map2.Instance.AddCrackFix();

        if (canvasObject != null)
        {
            canvasObject.SetActive(false);
        }

        // Disable this button GameObject itself
        gameObject.SetActive(false);
    }
}
