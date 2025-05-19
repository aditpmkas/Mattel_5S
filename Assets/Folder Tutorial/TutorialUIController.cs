    using System.Linq;
    using BNG;
    using UnityEngine;

    public class TutorialUIController : MonoBehaviour
    {
        [Header("Panels")]
        public GameObject welcomePanel;
        public GameObject confirmSkipPanel;

        public GameObject PauseMenuPanel;
        public GameObject ConfirmSkipPause;
        public Transform cameraRig;
        public float distanceFromHead = 1f;

        Grabbable[] allGrabbables;

        private void Awake()
        {
            allGrabbables = FindObjectsOfType<Grabbable>();
        }

        private void Start()
        {
        if (cameraRig == null && Camera.main != null)
            cameraRig = Camera.main.transform;

            DisableGrabbable();
            welcomePanel.SetActive(true);
            confirmSkipPanel.SetActive(false);
        
            PauseMenuPanel.SetActive(false);
            ConfirmSkipPause.SetActive(false);
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
        public void OnPauseOpened()
        {
            PauseMenuPanel.SetActive(true);
            DisableGrabbable();   // nonaktifkan interaksi objek saat pause
        }
        public void OnPauseClosed()
        {
            PauseMenuPanel.SetActive(false);
            ConfirmSkipPause.SetActive(false);
            EnableGrabbable();
        }
        public void OnSkipPausePressed()
        {
            PauseMenuPanel.SetActive(false);

            // Reposition ConfirmSkipPause di depan kepala
            if (ConfirmSkipPause != null && cameraRig != null)
            {
                ConfirmSkipPause.transform.position = cameraRig.position + cameraRig.forward * distanceFromHead;
                ConfirmSkipPause.transform.rotation = Quaternion.LookRotation(cameraRig.forward);
            }

            ConfirmSkipPause.SetActive(true);
        }

        public void OnConfirmSkipPauseYes()
        {
            // Tandai semua task selesai
            TaskManager.Instance.CompleteAllTasks();

            // Tutup panel konfirmasi dan panel pause
            ConfirmSkipPause.SetActive(false);
            PauseMenuPanel.SetActive(false);

            // Pastikan interaksi kembali aktif
            EnableGrabbable();
        }
        public void OnConfirmSkipPauseYes2()
        {
            // Tandai semua task selesai
            TaskManagerM2.Instance.CompleteAllTasks();

            // Tutup panel konfirmasi dan panel pause
            ConfirmSkipPause.SetActive(false);
            PauseMenuPanel.SetActive(false);

            // Pastikan interaksi kembali aktif
            EnableGrabbable();
        }

        public void OnConfirmSkipPauseNo()
        {
            ConfirmSkipPause.SetActive(false);

            // Reposition PauseMenuPanel kembali
            if (PauseMenuPanel != null && cameraRig != null)
            {
                PauseMenuPanel.transform.position = cameraRig.position + cameraRig.forward * distanceFromHead;
                PauseMenuPanel.transform.rotation = Quaternion.LookRotation(cameraRig.forward);
            }

            PauseMenuPanel.SetActive(true);
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
