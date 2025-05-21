using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManagerLevel2Map2 : MonoBehaviour
{
    public static ProgressManagerLevel2Map2 Instance;

    // Sorting task
    public int totalTrashCount = 4;
    public int sortedTrashCount = 0;

    // Set In Order task (Tools)
    public int totalToolsCount = 12;
    public int sortedToolsCount = 0;

    // Shine task
    public int totalPuddlesCount = 7;
    public int cleanedPuddlesCount = 0;

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
}
