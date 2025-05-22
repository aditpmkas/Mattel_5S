using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManagerLevel2Map2 : MonoBehaviour
{
    public static ProgressManagerLevel2Map2 Instance;

    // Sorting task
    public int totalTrashCount = 3;
    public int sortedTrashCount = 0;

    // Set In Order task (Tools)
    public int totalToolsCount = 12;
    public int sortedToolsCount = 0;

    // Shine task
    public int totalPuddlesCount = 4;
    public int cleanedPuddlesCount = 0;

    public int totalBiasItemsCount = 3;
    public int sortedBiasItemsCount = 0;

    public int totalCracksCount = 1;
    public int fixedCracksCount = 0;

    public int totalRootCauseCount = 3;
    public int cleanedRootCauseCount = 0;

    public bool hammerReturned = true;
    public bool mopReturned = true;
    // Task completion flags
    public bool sortingDone = false;
    public bool setInOrderDone = false;
    public bool shineDone = false;

    // Scoring
    public int totalScore = 0;
    public int maxPossibleScore = 2300;
    public string finalTime = "";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int score)
    {
        totalScore += score;
        Debug.Log("Total Score (Map 2): " + totalScore);
    }

    public void SubtractScore(int score)
    {
        totalScore -= score;
        Debug.Log("Total Score (Map 2): " + totalScore);
    }

    public void AddCrackFix()
    {
        fixedCracksCount++;
        Debug.Log("Fixed cracks: " + fixedCracksCount + " / " + totalCracksCount);
        CheckShineTaskComplete();
    }

    public void CheckShineTaskComplete()
    {
        if (cleanedPuddlesCount >= totalPuddlesCount
            && fixedCracksCount >= totalCracksCount
            && cleanedRootCauseCount >= totalRootCauseCount)
        {
            shineDone = true;
            Debug.Log("Shine task complete!");
        }
    }

    public void ResetProgress()
    {
        // Sorting task
        totalTrashCount = 3;
        sortedTrashCount = 0;

        // Set In Order task (Tools)
        totalToolsCount = 12;
        sortedToolsCount = 0;

        // Shine task
        totalPuddlesCount = 4;
        cleanedPuddlesCount = 0;

        totalBiasItemsCount = 3;
        sortedBiasItemsCount = 0;

        totalCracksCount = 1;
        fixedCracksCount = 0;

        totalRootCauseCount = 3;
        cleanedRootCauseCount = 0;

        hammerReturned = true;
        mopReturned = true;
        // Task completion flags
        sortingDone = false;
        setInOrderDone = false;
        shineDone = false;

        // Scoring
        totalScore = 0;
        maxPossibleScore = 2300;
        finalTime = "";
    }

}
