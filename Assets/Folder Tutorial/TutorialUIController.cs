using UnityEngine;

public class TutorialUIController : MonoBehaviour
{
    [Header("Panels")]
    public GameObject welcomePanel;
    public GameObject confirmSkipPanel;

    private void Start()
    {
        welcomePanel.SetActive(true);
        confirmSkipPanel.SetActive(false);
    }

    public void OnClosePressed ()
    {
        welcomePanel.SetActive(false);
        confirmSkipPanel.SetActive(false);
    }

    public void OnSkipPressed()
    {
        welcomePanel.SetActive(false);
        confirmSkipPanel.SetActive(true);
    }

    public void OnConfirmSkipYes()
    {
        TaskManager.Instance.CompleteAllTasks();
        confirmSkipPanel.SetActive(false);
    }

    public void OnConfirmSkipNo()
    {
        confirmSkipPanel.SetActive(false);
        welcomePanel.SetActive(true);
    }
}
