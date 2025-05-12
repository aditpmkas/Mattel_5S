using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum TaskType { Sorting, SetInOrder, Shine }

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance;

    [Header("Completion Notification Canvas")]
    public GameObject completionNotificationCanvas;

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
            onAllTasksCompleted?.Invoke();
        }
    }

    private void ShowCompletionNotification()
    {
        if (completionNotificationCanvas != null)
            completionNotificationCanvas.SetActive(true);
        Debug.Log("Tutorial completed! Showing completion notification.");
    }

    /// <summary>
    /// Check if a particular task has been done
    /// </summary>
    public bool IsTaskDone(TaskType task)
    {
        return completedTasks.Contains(task);
    }
}
