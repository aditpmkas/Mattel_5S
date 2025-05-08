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

        // Trigger phase transition jika kedua kondisi terpenuhi
        if (noSortsLeft && currentCorrectSorts >= requiredCorrectSorts)
        {
            TaskManager.Instance.CompleteTask(TaskType.Sorting);
            Destroy(this); // tidak perlu dicek terus
        }
    }

    public void IncrementCorrectSort()
    {
        currentCorrectSorts++;
    }
}
