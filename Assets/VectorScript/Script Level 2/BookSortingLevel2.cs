using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookSortingLevel2 : MonoBehaviour
{
    public string expectedBookTag;
    public int correctScoreValue = 100;
    public int wrongScoreValue = 50;

    private int currentScore = 0;

    private Dictionary<Collider, int> bookScores = new Dictionary<Collider, int>();

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("BookA") && !other.CompareTag("BookB") && !other.CompareTag("BookC") && !other.CompareTag("BookD"))
            return;

        int earnedScore = 0;

        // If sorting isn't done yet, penalize by 25
        if (!ProgressManagerLevel2.Instance.sortingDone)
        {
            earnedScore = (other.CompareTag(expectedBookTag)) ? correctScoreValue - 25 : wrongScoreValue - 25;
        }
        else
        {
            earnedScore = (other.CompareTag(expectedBookTag)) ? correctScoreValue : wrongScoreValue;
        }

        currentScore += earnedScore;
        ProgressManagerLevel2.Instance.AddScore(earnedScore);
        bookScores[other] = earnedScore;

        Debug.Log("Placed " + other.tag + " in " + gameObject.name + ". Score: " + currentScore);
    }

    private void OnTriggerExit(Collider other)
    {
        if (bookScores.ContainsKey(other))
        {
            int deductedScore = bookScores[other];
            currentScore -= deductedScore;
            ProgressManagerLevel2.Instance.SubtractScore(deductedScore);
            Debug.Log("Removed " + other.tag + " from " + gameObject.name + ". Score: " + currentScore);

            bookScores.Remove(other);
        }
    }
}
