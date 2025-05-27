using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using BNG;

public enum TaskType { Sorting, SetInOrder, Shine }

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance;

    [Header("Completion Notification Canvas")]
    public GameObject completionNotificationCanvas;

    public GameObject sortingCanvas, setInOrderCanvas, shineCanvas, GameMenu, ConfirmSkip;

    private HashSet<TaskType> completedTasks = new HashSet<TaskType>();
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

    public void CompleteTask(TaskType task)
    {
        if (completedTasks.Contains(task))
            return;

        completedTasks.Add(task);
        Debug.Log($"Task completed: {task}");

        if (completedTasks.Count >= System.Enum.GetValues(typeof(TaskType)).Length)
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
        Destroy(GameMenu);
        Destroy(ConfirmSkip);
    }

    public bool IsTaskDone(TaskType task)
    {
        return completedTasks.Contains(task);
    }

    public void CompleteAllTasks()
    {
        completedTasks = new HashSet<TaskType>(
        System.Enum.GetValues(typeof(TaskType)).Cast<TaskType>());
        ShowCompletionNotification();
        onAllTasksCompleted.Invoke();
    }

    public void ResetTask(TaskType task)
    {
        if (completedTasks.Remove(task))
            Debug.Log($"Task reset: {task}");
    }
}
