    using System.Linq;
    using BNG;
    using UnityEngine;

    public class TutorialUIController : MonoBehaviour
    {
        [Header("Panels")]
        public GameObject welcomePanel;
        public GameObject confirmSkipPanel;

        public GameObject GameMenuPanel;
        public GameObject ConfirmSkip; 

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
            GameMenuPanel.SetActive(false);
            ConfirmSkip.SetActive(false);
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

        public void OnSkipMenuPressed()
        {
            GameMenuPanel.SetActive(false);
            ConfirmSkip.SetActive(true);
        }

        public void OnConfirmSkipMenuYes()
        {
            TaskManager.Instance.CompleteAllTasks();

            ConfirmSkip.SetActive(false);
            GameMenuPanel.SetActive(false);

            DisableGrabbable();
        }

        public void OnConfirmSkipMenuYes2()
        {
            TaskManagerM2.Instance.CompleteAllTasks();

            ConfirmSkip.SetActive(false);
            GameMenuPanel.SetActive(false);
     
            DisableGrabbable();
        }

        public void OnConfirmSkipMenuNo()
        {
            ConfirmSkip.SetActive(false);
            GameMenuPanel.SetActive(true);
        }

        public void OnClosePressed()
        {
            welcomePanel.SetActive(false);
            confirmSkipPanel.SetActive(false);
            GameMenuPanel.SetActive(true);
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
