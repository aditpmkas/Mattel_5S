using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class SetInOrderM2 : MonoBehaviour
{
    public static SetInOrderM2 Instance;
    [Tooltip("Total jumlah tools yang harus disnap (misal 12)")]
    public int totalTools = 12;

    [Tooltip("Event Unity yang dipanggil saat semua tool tersnap")]
    public UnityEvent onAllToolsSnapped;

    // internal
    private bool isCompleted = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    /// <summary>
    /// Cek semua SnapPointTutorial dan update TaskManager.
    /// </summary>
    public void CheckAllSnapPointsTutorial()
    {
        var snapPoints = FindObjectsOfType<SnapPointTutorial>();
        // hitung yang benar tersnap
        int correct = snapPoints.Count(sp => sp.IsCorrectlySnapped);

        Debug.Log($"[SetInOrder] Progress: {correct}/{totalTools}");

        // selesai?
        if (correct == totalTools && !isCompleted)
        {
            isCompleted = true;
            Debug.Log("[SetInOrder] All snapped! Completing task.");
            TaskManagerM2.Instance.CompleteTask(TaskType2.SetInOrder);
            onAllToolsSnapped.Invoke();
        }
        // jika turun dari completed, reset
        else if (correct < totalTools && isCompleted)
        {
            isCompleted = false;
            Debug.Log("[SetInOrder] Snapped count dropped! Resetting task.");
            //TaskManagerM2.Instance.ResetTask(TaskType.SetInOrder);
        }
    }
}
