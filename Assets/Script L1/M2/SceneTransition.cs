using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    public void GoToLevel1Map1()
    {
        LoadingScreenManager.LoadScene("L1 M1");
    }

    public void GoToMainMenu()
    {
        LoadingScreenManager.LoadScene("MainMenu");
    }

    public void GoToTutorialMap1()
    {
        LoadingScreenManager.LoadScene("T M1");
    }

    public void GoToLevel1Map2()
    {
        LoadingScreenManager.LoadScene("L1 M2");
    }

    public void GoToTutorialMap2()
    {
        LoadingScreenManager.LoadScene("T M2");
    }

    public void GoToLevel2Map2()
    {
        LoadingScreenManager.LoadScene("L2 M2");
    }

    public void GoToLevel2Map1()
    {
        LoadingScreenManager.LoadScene("L2 M1");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
