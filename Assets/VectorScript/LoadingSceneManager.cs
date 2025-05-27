using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadingScreenManager : MonoBehaviour
{
    public static LoadingScreenManager Instance;

    [Header("UI References")]
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration = 1f;
    public GameObject loadingPanel;

    private bool isLoading = false;

    void Awake()
    {
        // Singleton + persist
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // BIKIN TETAP HIDUP
        }
        else
        {
            Destroy(gameObject); // jika duplikat
        }
    }

    private void Start()
    {
        if (fadeCanvasGroup != null)
        {
            fadeCanvasGroup.alpha = 0;
            fadeCanvasGroup.blocksRaycasts = false;
        }

        if (loadingPanel != null)
        {
            loadingPanel.SetActive(false);
        }
    }

    public static void LoadScene(string sceneName)
    {
        if (Instance != null && !Instance.isLoading)
        {
            Instance.StartCoroutine(Instance.LoadSceneRoutine(sceneName));
        }
    }

    private IEnumerator LoadSceneRoutine(string sceneName)
    {
        isLoading = true;

        // Fade Out
        if (loadingPanel != null)
            loadingPanel.SetActive(true);

        yield return StartCoroutine(Fade(1));

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        while (asyncLoad.progress < 0.9f)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.5f); // optional delay
        asyncLoad.allowSceneActivation = true;

        yield return new WaitForSeconds(0.3f); // optional delay after load
        yield return StartCoroutine(Fade(0));

        if (loadingPanel != null)
            loadingPanel.SetActive(false);

        isLoading = false;
    }

    private IEnumerator Fade(float targetAlpha)
    {
        fadeCanvasGroup.blocksRaycasts = true;

        float speed = Mathf.Abs(fadeCanvasGroup.alpha - targetAlpha) / fadeDuration;

        while (!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha))
        {
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
            yield return null;
        }

        fadeCanvasGroup.alpha = targetAlpha;
        fadeCanvasGroup.blocksRaycasts = (targetAlpha != 0);
    }
}
