using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultSceneUILevel2Map2 : MonoBehaviour
{
    public TMP_Text finalTimeText;
    public TMP_Text finalPercentageText;
    public TMP_Text finalGradeText;
    public TMP_Text gradeDescriptionText;

    void Start()
    {
        if (ProgressManagerLevel2Map2.Instance != null)
        {
            finalTimeText.text = ProgressManagerLevel2Map2.Instance.finalTime;

            // Calculate percentage
            float percentage = ((float)ProgressManagerLevel2Map2.Instance.totalScore / ProgressManagerLevel2Map2.Instance.maxPossibleScore) * 100f;
            percentage = Mathf.Clamp(percentage, 0, 100);

            finalPercentageText.text = percentage.ToString("F0") + "%";

            // Calculate grade
            string grade = CalculateGrade(ProgressManagerLevel2Map2.Instance.finalTime, percentage);
            finalGradeText.text = grade;

            // Set description in Bahasa Indonesia
            gradeDescriptionText.text = GetDescriptionForGrade(grade);
        }
        else
        {
            finalTimeText.text = "00:00";
            finalPercentageText.text = "0%";
            finalGradeText.text = "F";
            gradeDescriptionText.text = "TIDAK LULUS";
            Debug.LogWarning("ProgressManagerLevel2Map2 not found!");
        }
    }

    string CalculateGrade(string timeString, float percentage)
    {
        string[] parts = timeString.Split(':');
        int minutes = int.Parse(parts[0]);
        int seconds = int.Parse(parts[1]);
        float totalMinutes = minutes + (seconds / 60f);

        if (totalMinutes <= 3f &&
            percentage >= 90f &&
            ProgressManagerLevel2Map2.Instance.sortedBiasItemsCount == ProgressManagerLevel2Map2.Instance.totalBiasItemsCount &&
            ProgressManagerLevel2Map2.Instance.mopReturned == true &&
            ProgressManagerLevel2Map2.Instance.hammerReturned == true)
        {
            return "A+";
        }

        if (totalMinutes <= 3f && percentage >= 90f)
            return "A";
        else if (totalMinutes <= 4f && percentage >= 80f)
            return "B";
        else if (totalMinutes <= 6f && percentage >= 70f)
            return "C";
        else if (totalMinutes <= 8f && percentage >= 60f)
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
