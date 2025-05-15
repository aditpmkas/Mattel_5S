using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialCompleteUI : MonoBehaviour
{
    [Header("Button References")]
    public Button btnMainMenu;
    public Button btnLevel1Map1;
    public Button btnLevel2Map1;
    public Button btnLevel1Map2;
    public Button btnLevel2Map2;
    public Button btnTutorialMap1;
    public Button btnTutorialMap2;

    private void Awake()
    {
        // Pastikan semua Button di-assign lewat Inspector
        btnMainMenu.onClick.AddListener(GoToMainMenu);
        btnLevel1Map1.onClick.AddListener(GoToLevel1Map1);
        btnLevel2Map1.onClick.AddListener(GoToLevel2Map1);
        btnLevel1Map2.onClick.AddListener(GoToLevel1Map2);
        btnLevel2Map2.onClick.AddListener(GoToLevel2Map2); 
        btnTutorialMap1.onClick.AddListener(GoToTutorialMap1); 
        btnTutorialMap2.onClick.AddListener(GoToTutorialMap2); 
}

    private void OnDestroy()
    {
        // Lepas listener untuk mencegah duplikat saat scene reload
        btnMainMenu.onClick.RemoveListener(GoToMainMenu);
        btnLevel1Map1.onClick.RemoveListener(GoToLevel1Map1);
        btnLevel2Map1.onClick.RemoveListener(GoToLevel2Map1);
        btnLevel1Map2.onClick.RemoveListener(GoToLevel1Map2);
        btnLevel2Map2.onClick.RemoveListener(GoToLevel2Map2);
        btnTutorialMap1.onClick.RemoveListener(GoToTutorialMap1);
        btnTutorialMap2.onClick.RemoveListener(GoToTutorialMap2);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToLevel1Map1()
    {
        SceneManager.LoadScene("L1 M1");
    }

    public void GoToLevel2Map1()
    {
        SceneManager.LoadScene("L2 M1");
    }
    public void GoToLevel1Map2()
    {
        SceneManager.LoadScene("L1 M2");
    }
    public void GoToLevel2Map2()
    {
        SceneManager.LoadScene("L2 M2");
    }
    public void GoToTutorialMap1()
    {
        SceneManager.LoadScene("T M1");
    }
    public void GoToTutorialMap2()
    {
        SceneManager.LoadScene("T M2");
    }
}
