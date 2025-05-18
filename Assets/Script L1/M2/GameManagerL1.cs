using UnityEngine;

public class GameManagerLevel1 : MonoBehaviour
{
    public static GameManagerLevel1 Instance;

    public int totalSnapPointsToCheck = 6; // Jumlah SnapPoint di scene
    private bool isCompleted = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void CheckAllSnapPoints()
    {
        if (isCompleted) return;

        SnapPointLevel1[] snapPoints = FindObjectsOfType<SnapPointLevel1>();
        int correctCount = 0;

        foreach (var point in snapPoints)
        {
            if (point.isOccupied && point.snappedObject == point.correctObject)
            {
                correctCount++;
            }
        }

        if (correctCount == totalSnapPointsToCheck)
        {
            isCompleted = true;
            Debug.Log("Set In Order telah selesai!");
            // Bisa ditambah event lain di sini jika ingin trigger sesuatu setelah selesai
        }
    }
}
