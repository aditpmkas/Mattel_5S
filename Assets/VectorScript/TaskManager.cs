using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum TaskType { Sorting, SetInOrder, Shine }

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance;

    private HashSet<TaskType> completedTasks = new HashSet<TaskType>();

    public UnityEvent onAllTasksCompleted;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void CompleteTask(TaskType task)
    {
        if (!completedTasks.Contains(task))
        {
            completedTasks.Add(task);
            Debug.Log($"Task completed: {task}");

            if (completedTasks.Count >= 3)
                onAllTasksCompleted?.Invoke();
        }
    }

    public bool IsTaskDone(TaskType task)
    {
        return completedTasks.Contains(task);
    }
}
