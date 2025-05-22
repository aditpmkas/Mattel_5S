using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManagerLevel2 : MonoBehaviour
{
    public static ProgressManagerLevel2 Instance;

    public int totalTrashCount = 4;
    public int sortedTrashCount = 0;

    public int totalBooksCount = 8;
    public int sortedBooksCount = 0;

    public int totalPuddlesCount = 3;
    public int cleanedPuddlesCount = 0;

    public int totalBiasItemsCount = 3;
    public int sortedBiasItemsCount = 0;

    public int totalCracksCount = 3;
    public int fixedCracksCount = 0;

    public bool hammerReturned = true;
    public bool mopReturned = true;
    public bool sortingDone = false;
    public bool setInOrderDone = false;
    public bool shineDone = false;

    public int totalScore = 0;
    public int maxPossibleScore = 1800;
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
        Debug.Log("Total Score: " + totalScore);
    }

    public void SubtractScore(int score)
    {
        totalScore -= score;
        if (totalScore < 0) totalScore = 0;
        Debug.Log("Total Score: " + totalScore);
    }

    // Called by crack buttons
    public void AddCrackFix()
    {
        fixedCracksCount++;
        Debug.Log("Fixed cracks: " + fixedCracksCount + " / " + totalCracksCount);

        CheckShineTaskComplete();
    }

    // Shine task now requires all puddles cleaned + all cracks fixed
    public void CheckShineTaskComplete()
    {
        if (cleanedPuddlesCount >= totalPuddlesCount && fixedCracksCount >= totalCracksCount)
        {
            shineDone = true;
            Debug.Log("Shine task complete!");
        }
    }
}
