using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;               // Reference to the Text UI to display time
    public float timeRemaining = 60f;    // Starting time in seconds
    private bool timerRunning = false;   // Is the timer active?

    void Update()
    {
        // Only count down if the timer is running
        if (timerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay();
            }
            else
            {
                timeRemaining = 0;
                timerRunning = false;
                Debug.Log("Time's up!");
            }
        }
    }

    // Call this method to start the timer
    public void StartTimer()
    {
        timerRunning = true;
        Debug.Log("Timer started!");
    }

    // Updates the timer text on screen
    void UpdateTimerDisplay()
    {
        timerText.text = Mathf.CeilToInt(timeRemaining).ToString();
    }

}
