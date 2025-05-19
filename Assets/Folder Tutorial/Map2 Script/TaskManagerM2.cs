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

    public GameObject sortingCanvas, setInOrderCanvas, shineCanvas;

    private HashSet<TaskType2> completedTasks = new HashSet<TaskType2>();
    public UnityEvent onAllTasksCompleted;

    public PauseMenuController pauseMenuController;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        if (completionNotificationCanvas != null)
            completionNotificationCanvas.SetActive(false);
    }

    /// <summary>
    /// Mark a task as complete. When all tasks are done, show final notification.
    /// </summary>
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

        Debug.Log("Tutorial completed! Showing completion notification.");

        Destroy(sortingCanvas);
        Destroy(setInOrderCanvas);
        Destroy(shineCanvas);

        if (pauseMenuController != null)
        {
            pauseMenuController.DisablePauseMenu();
        }
    }

    /// <summary>
    /// Check if a particular task has been done
    /// </summary>
    public bool IsTaskDone(TaskType2 task)
    {
        return completedTasks.Contains(task);
    }

    /// <summary>
    /// Force‐mark all tasks as complete and fire the completion event.
    /// </summary>
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
