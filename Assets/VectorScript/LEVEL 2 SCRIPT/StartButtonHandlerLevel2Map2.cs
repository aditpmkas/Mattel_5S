using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonHandlerLevel2Map2 : MonoBehaviour
{
    public TimerLevel2Map2 gameTimer; // Reference to the Timer script in your scene

    // Call this when the button is clicked
    public void OnStartButtonPressed()
    {
        if (gameTimer != null)
        {
            gameTimer.StartTimer();
        }
        else
        {
            Debug.Log("GameTimer reference not set on StartButtonHandler!");
        }
    }
}
