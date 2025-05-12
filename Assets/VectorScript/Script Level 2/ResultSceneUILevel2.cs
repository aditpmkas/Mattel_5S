using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultSceneUILevel2 : MonoBehaviour
{
    public Text totalScoreText;
    public Text finalTimeText;  // Drag your Timer Text UI here in Inspector

    void Start()
    {
        if (ProgressManagerLevel2.Instance != null)
        {
            totalScoreText.text = "Total Score: " + ProgressManagerLevel2.Instance.totalScore.ToString();
            finalTimeText.text = "Time: " + ProgressManagerLevel2.Instance.finalTime;
        }
        else
        {
            totalScoreText.text = "Total Score: 0";
            finalTimeText.text = "Time: 00:00";
            Debug.LogWarning("ProgressManagerLevel2 not found!");
        }
    }
}
