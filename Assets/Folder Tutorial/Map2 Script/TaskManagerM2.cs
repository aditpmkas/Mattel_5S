using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using BNG;

public enum TaskType2 { Sorting, SetInOrder, Shine }

public class TaskManagerM2 : MonoBehaviour
{
    public static TaskManagerM2 Instance;

    [Header("Completion Notification Canvas")]
    public GameObject completionNotificationCanvas;

    public GameObject sortingCanvas, setInOrderCanvas, shineCanvas, gameMenu, confirmSkip;

    private HashSet<TaskType2> completedTasks = new HashSet<TaskType2>();
    public UnityEvent onAllTasksCompleted;

    [Header("Audio")]
    public AudioClip completionSound;
    public AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Start()
    {
        if (completionNotificationCanvas != null)
            completionNotificationCanvas.SetActive(false);
    }

    public void CompleteTask(TaskType2 task)
    {
        if (completedTasks.Contains(task))
            return;

         completedTasks.Add(task);
        Debug.Log($"Task completed: {task}");

        if (completedTasks.Count >= System.Enum.GetValues(typeof(TaskType2)).Length)
        {
            ShowCompletionNotification();
            onAllTasksCompleted.Invoke();
        }
    }

    private void ShowCompletionNotification()
    {
        var ui = FindObjectOfType<TutorialUIController>();
        if (ui != null) ui.DisableGrabbable();

        if (completionNotificationCanvas != null)
            completionNotificationCanvas.SetActive(true);

        if (completionSound != null && audioSource != null)
        audioSource.PlayOneShot(completionSound);

        Debug.Log("Tutorial completed! Showing completion notification.");

        Destroy(sortingCanvas);
        Destroy(setInOrderCanvas);
        Destroy(shineCanvas);
        Destroy(gameMenu);
        Destroy(confirmSkip);
    }

    public bool IsTaskDone(TaskType2 task)
    {
        return completedTasks.Contains(task);
    }
    public void CompleteAllTasks()
    {
        completedTasks = new HashSet<TaskType2>(
        System.Enum.GetValues(typeof(TaskType2)).Cast<TaskType2>());
        ShowCompletionNotification();
        onAllTasksCompleted.Invoke();
    }
    public void ResetTask(TaskType2 task)
    {
        if (completedTasks.Remove(task))
            Debug.Log($"Task reset: {task}");
    }
}
