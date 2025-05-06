using System.Linq;
using UnityEngine;

public class SortingChecker : MonoBehaviour
{
    public GameObject table; // tempat objek Sort & Unsort
    public int requiredCorrectSorts = 4;
    private int currentCorrectSorts = 0;

    private void Update()
    {
        // Cek jika tidak ada lagi object dengan tag "Sort" di meja
        bool noSortsLeft = !table.GetComponentsInChildren<Transform>()
                                 .Any(child => child.CompareTag("Sort"));

        // Trigger phase transition jika kedua kondisi terpenuhi
        if (noSortsLeft && currentCorrectSorts >= requiredCorrectSorts)
        {
            GamePhaseManager.Instance.SetPhase(GamePhaseManager.Phase.SetInOrder);
            Destroy(this); // tidak perlu dicek terus
        }
    }

    public void IncrementCorrectSort()
    {
        currentCorrectSorts++;
    }
}
