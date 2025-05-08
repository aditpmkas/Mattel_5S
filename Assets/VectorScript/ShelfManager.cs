using UnityEngine;

public class ShelfManager : MonoBehaviour
{
    public static ShelfManager Instance;

    private BookShelfTutorial[] shelves;

    private void Awake()
    {
        Instance = this;
        shelves = FindObjectsOfType<BookShelfTutorial>();
    }

    public void CheckAllShelvesComplete()
    {
        foreach (var shelf in shelves)
        {
            if (!shelf.IsComplete())
                return;
        }

        Debug.Log("Semua rak selesai diisi!");
        TaskManager.Instance.CompleteTask(TaskType.SetInOrder);
    }
}
