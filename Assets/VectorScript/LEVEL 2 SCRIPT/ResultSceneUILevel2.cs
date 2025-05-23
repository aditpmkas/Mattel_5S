using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultSceneUILevel2 : MonoBehaviour
{
    //public Text totalScoreText;
    public TMP_Text finalTimeText;
    public TMP_Text finalPercentageText; // Drag your percentage Text UI here in Inspector
    public TMP_Text finalGradeText; // Drag your grade Text UI here in Inspector
    public TMP_Text gradeDescriptionText; // New TMP_Text for description

    void Start()
    {
        if (ProgressManagerLevel2.Instance != null)
        {
            //totalScoreText.text = "Total Score: " + ProgressManagerLevel2.Instance.totalScore.ToString();
            finalTimeText.text = ProgressManagerLevel2.Instance.finalTime;

            // Calculate percentage
            float percentage = ((float)ProgressManagerLevel2.Instance.totalScore / ProgressManagerLevel2.Instance.maxPossibleScore) * 100f;
            percentage = Mathf.Clamp(percentage, 0, 100); // Ensure it stays between 0 and 100

            finalPercentageText.text = percentage.ToString("F0") + "%";

            // Calculate grade
            string grade = CalculateGrade(ProgressManagerLevel2.Instance.finalTime, percentage);
            finalGradeText.text = grade;

            // Set description in Bahasa Indonesia based on grade
            gradeDescriptionText.text = GetDescriptionForGrade(grade);
        }
        else
        {
            //totalScoreText.text = "Total Score: 0";
            finalTimeText.text = "00:00";
            finalPercentageText.text = "0%";
            finalGradeText.text = "F";
            gradeDescriptionText.text = "TIDAK LULUS";
            Debug.LogWarning("ProgressManagerLevel2 not found!");
        }
    }

    // Calculate grade based on time and percentage
    string CalculateGrade(string timeString, float percentage)
    {
        string[] parts = timeString.Split(':');
        int minutes = int.Parse(parts[0]);
        int seconds = int.Parse(parts[1]);
        float totalMinutes = minutes + (seconds / 60f);

        //  Check for A+ first
        if (totalMinutes <= 2f &&
            percentage >= 90f &&
            ProgressManagerLevel2.Instance.sortedBiasItemsCount == ProgressManagerLevel2.Instance.totalBiasItemsCount &&
            ProgressManagerLevel2.Instance.mopReturned == true &&
            ProgressManagerLevel2.Instance.hammerReturned == true)
        {
            return "A+";
        }
        // Normal grading
        else if (totalMinutes <= 2f && percentage >= 90f)
            return "A";
        else if (totalMinutes <= 3f && percentage >= 80f)
            return "B";
        else if (totalMinutes <= 5f && percentage >= 70f)
            return "C";
        else if (totalMinutes <= 7f && percentage >= 60f)
            return "D";
        else if (totalMinutes <= 10f && percentage >= 50f)
            return "E";
        else
            return "F";
    }

    string GetDescriptionForGrade(string grade)
    {
        switch (grade)
        {
            case "A+": return "SEMPURNA";
            case "A": return "SANGAT BAIK";
            case "B": return "BAIK SEKALI";
            case "C": return "CUKUP";
            case "D": return "KURANG";
            case "E": return "SANGAT KURANG";
            case "F": return "TIDAK LULUS";
            default: return "-";
        }
    }

}
