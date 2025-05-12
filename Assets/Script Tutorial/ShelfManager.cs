using UnityEngine;
using TMPro;

public class ShelfManager : MonoBehaviour
{
    public static ShelfManager Instance;

    private int totalRequiredBooks = 0;

    private void Awake()
    {
        Instance = this;

        // Hitung total buku dari semua rak (versi BookShelfTutorial)
        BookShelfTutorial[] allShelves = FindObjectsOfType<BookShelfTutorial>();
        foreach (BookShelfTutorial shelf in allShelves)
        {
            totalRequiredBooks += shelf.requiredCount;
        }
    }

    public void CheckAllShelvesComplete()
    {
        BookShelfTutorial[] shelves = FindObjectsOfType<BookShelfTutorial>();
        int total = shelves.Length;
        int complete = 0;

        foreach (BookShelfTutorial shelf in shelves)
        {
            if (shelf.IsComplete()) complete++;
            else Debug.Log($"Rak belum selesai: {shelf.gameObject.name}");
        }

        Debug.Log($"Rak selesai: {complete}/{total}");

        if (complete == total)
        {
            Debug.Log("Semua rak sudah lengkap!");
            TaskManager.Instance.CompleteTask(TaskType.SetInOrder);
        }
    }


    public void NotifyBookPlaced()
    {
        CheckAllShelvesComplete();
    }
}
