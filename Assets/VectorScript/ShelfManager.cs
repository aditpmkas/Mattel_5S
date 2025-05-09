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
        {
            Debug.Log("[ShelfManager] Masih ada rak yang belum selesai!");
            return;
        }
    }

    Debug.Log("[ShelfManager] Semua rak selesai diisi!");
    TaskManager.Instance.CompleteTask(TaskType.SetInOrder);
 }
}
