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
    public TutorialUI tutorialUI;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    private void Start()
    {
        tutorialUI.UpdateUI("Sorting", "Buang barang tidak diperlukan ke tempat sampah.");
    }

    public void CompleteTask(TaskType task)
    {
        switch (task)
        {
            case TaskType.Sorting:
                tutorialUI.UpdateUI("Set in Order", "Letakkan buku pada rak yang sesuai.");
                break;
            case TaskType.SetInOrder:
                tutorialUI.UpdateUI("Shine", "Gunakan pel untuk membersihkan lantai kotor.");
                break;
            case TaskType.Shine:
                tutorialUI.UpdateUI("Selesai!", "Semua tugas selesai. Terima kasih!");
                break;
        }

        if (!completedTasks.Contains(task))
        {
            completedTasks.Add(task);
            Debug.Log("Task completed: {task}");

            if (completedTasks.Count >= 3)
                onAllTasksCompleted?.Invoke();
        }
    }

    public bool IsTaskDone(TaskType task)
    {
        return completedTasks.Contains(task);
    }
}
