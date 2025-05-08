using System.Linq;
using UnityEngine;

public class SortingChecker : MonoBehaviour
{
    public GameObject table; // tempat objek Sort & Unsort
    public int requiredCorrectSorts = 4;
    private int currentCorrectSorts = 0;

    private void Update()
    {
        if (table == null)
        {
            Debug.LogWarning("SortingChecker: 'table' belum di-assign di Inspector.");
            return;
        }

        // Cek jika tidak ada lagi object dengan tag "Sort" di meja
        bool noSortsLeft = !table.GetComponentsInChildren<Transform>()
                                 .Any(child => child.CompareTag("Sort"));

        // Trigger task selesai
        if (noSortsLeft && currentCorrectSorts >= requiredCorrectSorts)
        {
            Debug.Log("Sorting task selesai!");

            // Tandai task selesai (jika kamu pakai TaskManager)
            TaskManager.Instance?.MarkTaskComplete(TaskType.Sorting);

            Destroy(this); // Tidak perlu Update terus
        }
    }

    public void IncrementCorrectSort()
    {
        currentCorrectSorts++;
    }
}
