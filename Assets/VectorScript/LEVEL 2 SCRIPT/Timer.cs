using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText;               // Reference to the Text UI to display time
    public float targetTime = 600f;      // The time to reach (10 minutes here)
    private float timeElapsed = 0f;      // How much time has passed
    private bool timerRunning = false;   // Is the timer active?

    void Update()
    {
        if (timerRunning)
        {
            if (timeElapsed < targetTime)
            {
                timeElapsed += Time.deltaTime;
                UpdateTimerDisplay();
            }
            else
            {
                timeElapsed = targetTime;
                timerRunning = false;
                UpdateTimerDisplay(); // Make sure it shows final time exactly
                Debug.Log("Target time reached!");
            }
        }
    }

    public void StartTimer()
    {
        timerRunning = true;
        Debug.Log("Count-up timer started!");
    }

    void UpdateTimerDisplay()
    {
        // Convert timeElapsed to minutes and seconds
        int minutes = Mathf.FloorToInt(timeElapsed / 60);
        int seconds = Mathf.FloorToInt(timeElapsed % 60);

        // Update the text in MM:SS format
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StopTimer()
    {
        timerRunning = false;

        // Save final time string to ProgressManager
        if (ProgressManagerLevel2.Instance != null)
        {
            ProgressManagerLevel2.Instance.finalTime = timerText.text;
        }

        Debug.Log("Timer stopped at: " + timerText.text);
    }

}
