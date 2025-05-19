using System.Linq;
using UnityEngine;

public class SortingTutorialM2 : MonoBehaviour
{
    public GameObject trashCan; // tempat objek Sort & Unsort
    public int requiredCorrectSorts = 3;
    private int currentCorrectSorts = 0;

    private void Update()
    {
        // Cek jika tidak ada lagi object dengan tag "Sort" di meja
        bool noSortsLeft = !trashCan.GetComponentsInChildren<Transform>()
                                 .Any(child => child.CompareTag("Sort"));

        // Gunakan string interpolasi (pakai $)
        Debug.Log($"[SortingTutorial] CurrentCorrectSorts: {currentCorrectSorts}/{requiredCorrectSorts}, NoSortsLeft: {noSortsLeft}");

        // Trigger jika tugas selesai
        if (noSortsLeft && currentCorrectSorts >= requiredCorrectSorts)
        {
            Debug.Log("[SortingTutorial] Sorting task completed!");
            TaskManagerM2.Instance.CompleteTask(TaskType2.Sorting);
            Destroy(this); // Hapus komponen supaya tidak dicek terus
        }
    }

    public void IncrementCorrectSort()
    {
        currentCorrectSorts++;
        Debug.Log($"[SortingTutorial] Correct Sort Added! New Total: {currentCorrectSorts}");
    }
}
