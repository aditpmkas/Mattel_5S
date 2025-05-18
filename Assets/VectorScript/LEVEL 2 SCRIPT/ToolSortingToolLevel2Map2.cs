using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolSortingToolLevel2Map2 : MonoBehaviour
{
    public string expectedToolTag;
    public int expectedSizeOrder;

    private int lastScoreAdded = 0;

    public void OnSnap(GameObject snappedTool)
    {
        var toolProps = snappedTool.GetComponent<ToolPropertiesLevel2Map2>();
        if (toolProps == null) return;

        string toolTag = snappedTool.tag;
        int toolSizeOrder = toolProps.sizeOrder;
        int scoreToAdd = 0;

        //  Check if type is correct
        if (toolTag == expectedToolTag)
        {
            //  Check if Sorting is done
            if (ProgressManagerLevel2Map2.Instance.sortingDone)
            {
                // Correct Type
                if (toolSizeOrder == expectedSizeOrder)
                {
                    scoreToAdd = 100;
                    Debug.Log("Sorting done, correct tool & size (+100)");
                }
                else
                {
                    scoreToAdd = 75;
                    Debug.Log("Sorting done, correct tool but wrong size (+75)");
                }
            }
            else
            {
                // Sorting not done
                if (toolSizeOrder == expectedSizeOrder)
                {
                    scoreToAdd = 75;
                    Debug.Log("Sorting not done, correct tool & size (+75)");
                }
                else
                {
                    scoreToAdd = 50;
                    Debug.Log("Sorting not done, correct tool but wrong size (+50)");
                }
            }
        }
        else
        {
            //  Type is wrong — always 0 points
            scoreToAdd = 0;
            Debug.Log("Wrong tool type (0)");
        }

        // Apply score if greater than zero
        if (scoreToAdd > 0)
        {
            ProgressManagerLevel2Map2.Instance.AddScore(scoreToAdd);
        }

        lastScoreAdded = scoreToAdd;

        // Count this placed tool
        ProgressManagerLevel2Map2.Instance.sortedToolsCount++;
        Debug.Log("Tools placed: " + ProgressManagerLevel2Map2.Instance.sortedToolsCount);
    }

    public void OnRelease()
    {
        // Remove score if this snap point had one
        if (lastScoreAdded > 0)
        {
            ProgressManagerLevel2Map2.Instance.SubtractScore(lastScoreAdded);
        }

        // Always reduce sortedToolsCount if a tool was removed
        ProgressManagerLevel2Map2.Instance.sortedToolsCount--;
        Debug.Log("Tool removed, total sorted tools: " + ProgressManagerLevel2Map2.Instance.sortedToolsCount);

        lastScoreAdded = 0;
    }
}
