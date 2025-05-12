using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManagerLevel2 : MonoBehaviour
{
    public static ProgressManagerLevel2 Instance;

    public bool sortingDone = false;
    public bool setInOrderDone = false;
    public bool shineDone = false;
    public string finalTime = "";

    public int totalScore = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // <--- This line makes it persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Call this from other scripts to add to total score
    public void AddScore(int score)
    {
        totalScore += score;
        Debug.Log("Total Score: " + totalScore);
    }

    // Call this to subtract from total score
    public void SubtractScore(int score)
    {
        totalScore -= score;
        Debug.Log("Total Score: " + totalScore);
    }

}
