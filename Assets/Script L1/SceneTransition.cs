using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public void GoToLevel2Map1()
    {
        SceneManager.LoadScene("L2 M1");
    }
    public void GoToLevel1Map1()
    {
        SceneManager.LoadScene("L1 M1");
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void GoToTutorialMap1()
    {
        SceneManager.LoadScene("T M1");
    }
    public void GoToLevel2Map2()
    {
        SceneManager.LoadScene("L2 M2");
    }
    public void GoToLevel1Map2()
    {
        SceneManager.LoadScene("L1 M2");
    }
    public void GoToTutorialMap2()
    {
        SceneManager.LoadScene("T M2");
    }
    public void ExitGame()
    {
        Application.Quit(); // Keluar dari game jika di build
    }
}