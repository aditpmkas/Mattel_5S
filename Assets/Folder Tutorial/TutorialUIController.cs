using System.Linq;
using BNG;
using UnityEngine;

public class TutorialUIController : MonoBehaviour
{
    [Header("Panels")]
    public GameObject welcomePanel;
    public GameObject confirmSkipPanel;
    Grabbable[] allGrabbables;

    private void Awake()
    {
        allGrabbables = FindObjectsOfType<Grabbable>();
    }

    private void Start()
    {
        DisableGrabbable();
        welcomePanel.SetActive(true);
        confirmSkipPanel.SetActive(false);
    }

    public void EnableGrabbable ()
    {
        foreach (Grabbable grabbable in allGrabbables)
        {
            grabbable.enabled = true;
        }

    }
    public void DisableGrabbable()
    {
        allGrabbables = allGrabbables
        .Where(g => g != null)
        .ToArray();

        foreach (var grabbable in allGrabbables)
        {
            if (grabbable != null)    // extra check just in case
                grabbable.enabled = false;
        }

    }
    public void OnClosePressed ()
    {
        welcomePanel.SetActive(false);
        confirmSkipPanel.SetActive(false);
        EnableGrabbable();
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
    public void OnConfirmSkipYesMap2()
    {
        TaskManagerM2.Instance.CompleteAllTasks();
        confirmSkipPanel.SetActive(false);
    }

    public void OnConfirmSkipNo()
    {
        confirmSkipPanel.SetActive(false);
        welcomePanel.SetActive(true);
    }
}
