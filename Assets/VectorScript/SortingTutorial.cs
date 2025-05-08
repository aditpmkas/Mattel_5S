using System.Linq;
using UnityEngine;

public class SortingTutorial : MonoBehaviour
{
    public GameObject table_; // tempat objek Sort & Unsort
    public int requiredCorrectSorts = 4;
    private int currentCorrectSorts = 0;

    private void Update()
    {
        // Cek jika tidak ada lagi object dengan tag "Sort" di meja
        bool noSortsLeft = !table_.GetComponentsInChildren<Transform>()
                                 .Any(child => child.CompareTag("Sort"));

        // Debug log jumlah correct sort dan apakah ada Sort yang tersisa
        Debug.Log($"[SortingTutorial] CurrentCorrectSorts: {currentCorrectSorts}/{requiredCorrectSorts}, NoSortsLeft: {noSortsLeft}");

        // Trigger jika tugas selesai
        if (noSortsLeft && currentCorrectSorts >= requiredCorrectSorts)
        {
            Debug.Log("[SortingTutorial] Sorting task completed!");
            TaskManager.Instance.CompleteTask(TaskType.Sorting);
            Destroy(this); // Hapus komponen supaya tidak dicek terus
        }
    }

    public void IncrementCorrectSort()
    {
        currentCorrectSorts++;
        Debug.Log($"[SortingTutorial] Correct Sort Added! New Total: {currentCorrectSorts}");
    }
}
