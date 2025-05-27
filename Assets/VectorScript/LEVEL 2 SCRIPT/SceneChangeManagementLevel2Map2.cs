using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class SceneChangeManagementLevel2Map2 : MonoBehaviour
{
    public TimerLevel2Map2 gameTimer;         // Reference to your Timer script
    //public string sceneToLoad;      // Name of the scene to load on completion

    public void OnCompleteButtonPressed()
    {
        if (gameTimer != null)
        {
            gameTimer.StopTimer();
        }

        //Debug.Log("Loading scene: " + sceneToLoad);
        //SceneManager.LoadScene(sceneToLoad);
    }
}
