using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialCompleteUI : MonoBehaviour
{
    [Header("Set False Canvas")]
    public GameObject tutorialCompleteUICanvas;
    public GameObject skipMenuCanvas;
    public GameObject confirmSkipCanvas;
    public GameObject welcomeCanvas;

    [Header("Button References")]
    public Button btnMainMenu;
    public Button btnLevel1Map1;
    public Button btnLevel2Map1;
    public Button btnLevel1Map2;
    public Button btnLevel2Map2;

    private void Awake()
    {
        // Pastikan semua Button di-assign lewat Inspector
        btnMainMenu.onClick.AddListener(GoToMainMenu);
        btnLevel1Map1.onClick.AddListener(GoToLevel1Map1);
        btnLevel2Map1.onClick.AddListener(GoToLevel2Map1);
        btnLevel1Map2.onClick.AddListener(GoToLevel1Map2);
        btnLevel2Map2.onClick.AddListener(GoToLevel2Map2);
    }

    private void OnDestroy()
    {
        if (btnMainMenu != null)
            btnMainMenu.onClick.RemoveListener(GoToMainMenu);

        if (btnLevel1Map1 != null)
            btnLevel1Map1.onClick.RemoveListener(GoToLevel1Map1);

        if (btnLevel2Map1 != null)
            btnLevel2Map1.onClick.RemoveListener(GoToLevel2Map1);

        if (btnLevel1Map2 != null)
            btnLevel1Map2.onClick.RemoveListener(GoToLevel1Map2);

        if (btnLevel2Map2 != null)
            btnLevel2Map2.onClick.RemoveListener(GoToLevel2Map2);
    }

    public void GoToMainMenu()
    {  
        if (welcomeCanvas != null)
            welcomeCanvas.SetActive(false); 

        if (skipMenuCanvas !=  null)
            skipMenuCanvas.SetActive(false);

        if (confirmSkipCanvas != null) 
            confirmSkipCanvas.SetActive(false);

        if (tutorialCompleteUICanvas != null)
            tutorialCompleteUICanvas.SetActive(false);

        LoadingScreenManager.LoadScene("MainMenu");
    }

    public void GoToLevel1Map1()
    {
        if (welcomeCanvas != null)
            welcomeCanvas.SetActive(false);

        if (skipMenuCanvas != null)
            skipMenuCanvas.SetActive(false);

        if (confirmSkipCanvas != null)
            confirmSkipCanvas.SetActive(false);

        if (tutorialCompleteUICanvas != null)
            tutorialCompleteUICanvas.SetActive(false);

        LoadingScreenManager.LoadScene("L1 M1");
    }

    public void GoToLevel2Map1()
    {
        if (welcomeCanvas != null)
            welcomeCanvas.SetActive(false);

        if (skipMenuCanvas != null)
            skipMenuCanvas.SetActive(false);

        if (confirmSkipCanvas != null)
            confirmSkipCanvas.SetActive(false);

        if (tutorialCompleteUICanvas != null)
            tutorialCompleteUICanvas.SetActive(false);

        LoadingScreenManager.LoadScene("L2 M1");
    }

    public void GoToLevel1Map2()
    {
        if (welcomeCanvas != null)
            welcomeCanvas.SetActive(false);

        if (skipMenuCanvas != null)
            skipMenuCanvas.SetActive(false);

        if (confirmSkipCanvas != null)
            confirmSkipCanvas.SetActive(false);

        if (tutorialCompleteUICanvas != null)
            tutorialCompleteUICanvas.SetActive(false);

        LoadingScreenManager.LoadScene("L1 M2");
    }

    public void GoToLevel2Map2()
    {
        if (welcomeCanvas != null)
            welcomeCanvas.SetActive(false);

        if (skipMenuCanvas != null)
            skipMenuCanvas.SetActive(false);

        if (confirmSkipCanvas != null)
            confirmSkipCanvas.SetActive(false);

        if (tutorialCompleteUICanvas != null)
            tutorialCompleteUICanvas.SetActive(false);

        LoadingScreenManager.LoadScene("L2 M2");
    }
}
