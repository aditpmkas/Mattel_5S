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

    public GameObject sortingCanvas;
    public GameObject setInOrderCanvas;
    public GameObject shineCanvas;

    private HashSet<TaskType> completedTasks = new HashSet<TaskType>();
    public UnityEvent onAllTasksCompleted;

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

        Debug.Log("Tutorial completed! Showing completion notification.");

        Destroy(sortingCanvas);
        Destroy(setInOrderCanvas);
        Destroy(shineCanvas);
    }

    /// <summary>
    /// Check if a particular task has been done
    /// </summary>
    public bool IsTaskDone(TaskType task)
    {
        return completedTasks.Contains(task);
    }

    /// <summary>
    /// Force‐mark all tasks as complete and fire the completion event.
    /// </summary>
    public void CompleteAllTasks()
    {
        // If you don’t care about duplicate events, you can just
        // call CompleteTask on every enum value:
        //foreach (TaskType t in System.Enum.GetValues(typeof(TaskType)).Cast<TaskType>())
        //CompleteTask(t);

        // In case you want to skip the logging/duplicate-check in CompleteTask,
        // you could instead do:
        completedTasks = new HashSet<TaskType>(
        System.Enum.GetValues(typeof(TaskType)).Cast<TaskType>());
        ShowCompletionNotification();
        onAllTasksCompleted.Invoke();
    }
}
