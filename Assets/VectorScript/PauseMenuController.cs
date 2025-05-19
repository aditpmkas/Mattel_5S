using UnityEngine;
using BNG;

public class PauseMenuController : MonoBehaviour
{
    [Header("Assign Pause Canvas and (optionally) Camera Rig")]
    public GameObject pauseMenuCanvas;
    public Transform cameraRig;         // Boleh assign manual ke CenterEyeAnchor
    public float distanceFromHead = 1f; // Jarak menu dari kepala saat pause

    bool isPaused = false;

    TutorialUIController tutorialUI;

    void Start()
    {
        // Cache TutorialUIController sekali saja
        tutorialUI = FindObjectOfType<TutorialUIController>();

        // Jika belum assign cameraRig lewat Inspector, coba ambil Camera.main
        if (cameraRig == null && Camera.main != null)
            cameraRig = Camera.main.transform;

        // Pastikan canvas awalnya tertutup
        if (pauseMenuCanvas != null)
            pauseMenuCanvas.SetActive(false);

        Time.timeScale = 1f;
    }

    void Update()
    {
        // Toggle pause jika controller Start atau Escape ditekan
        if (InputBridge.Instance.StartButtonDown || Input.GetKeyDown(KeyCode.Escape))
            TogglePause();
    }

    void TogglePause()
    {
        isPaused = !isPaused;

        // Reset waktu
        Time.timeScale = isPaused ? 0f : 1f;

        if (pauseMenuCanvas != null)
        {
            // Posisi ulang menu setiap pause
            if (isPaused && cameraRig != null)
            {
                pauseMenuCanvas.transform.position = cameraRig.position + cameraRig.forward * distanceFromHead;
                pauseMenuCanvas.transform.rotation = Quaternion.LookRotation(cameraRig.forward);
            }

            pauseMenuCanvas.SetActive(isPaused);
        }

        // Notify TutorialUIController
        if (tutorialUI != null)
        {
            if (isPaused) tutorialUI.OnPauseOpened();
            else tutorialUI.OnPauseClosed();
        }
    }

    /// <summary>
    /// Disable ability to pause (dipanggil saat task complete).
    /// </summary>
    public void DisablePauseMenu()
    {
        // Sembunyikan canvas jika sedang terbuka
        if (pauseMenuCanvas != null)
            pauseMenuCanvas.SetActive(false);

        // Matikan script ini
        enabled = false;

        // Pastikan time berjalan
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Dipanggil oleh tombol “Resume” pada UI.
    /// </summary>
    public void OnResumeButton()
    {
        if (isPaused)
            TogglePause();
    }

    /// <summary>
    /// Dipanggil oleh tombol “Main Menu” pada UI.
    /// </summary>
    public void OnMainMenuButton()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
    public void OnMainMenuButton2()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
