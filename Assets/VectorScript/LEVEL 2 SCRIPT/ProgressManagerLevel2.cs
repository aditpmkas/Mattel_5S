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

    public bool sortingDone = false;
    public bool setInOrderDone = false;
    public bool shineDone = false;

    public int totalScore = 0;
    public int maxPossibleScore = 1500;
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
        Debug.Log("Total Score: " + totalScore);
    }

}
