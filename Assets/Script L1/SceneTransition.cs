using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public void GoToLevel2()
    {
        SceneManager.LoadScene("L2 M1");
    }
    public void GoToLevel1()
    {
        SceneManager.LoadScene("L1 M1");
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void GoToTutorial()
    {
        SceneManager.LoadScene("T M1");
    }
} 