using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class SetInOrderM2 : MonoBehaviour
{
    public static SetInOrderM2 Instance;

    [Header("Delay Settings")]
    [Tooltip("Delay in seconds before marking SetInOrder complete on Map2")]
    public float setInOrderDelay = 1f;

    [Tooltip("Total jumlah tools yang harus disnap (misal 12)")]
    public int totalTools = 12;
    public UnityEvent onAllToolsSnapped;

    // Internal state
    private bool isCompleted = false;
    private Coroutine pendingCompletion;

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
        int correct = snapPoints.Count(sp => sp.IsCorrectlySnapped);

        Debug.Log($"[SetInOrderM2] Progress: {correct}/{totalTools}");

        // Semua ter-snap
        if (correct == totalTools)
        {
            // Jika belum complete dan belum ada pending, jadwalkan complete
            if (!isCompleted && pendingCompletion == null)
            {
                Debug.Log($"[SetInOrderM2] Semua tools tersnap, akan complete dalam {setInOrderDelay} detik");
                pendingCompletion = StartCoroutine(DelayedComplete());
            }
        }
        else
        {
            // Batalkan pending jika ada
            if (pendingCompletion != null)
            {
                Debug.Log("[SetInOrderM2] Pembatalan pending complete karena tools terlepas");
                StopCoroutine(pendingCompletion);
                pendingCompletion = null;
            }

            // Reset jika sudah sempat complete sebelumnya
            if (isCompleted)
            {
                isCompleted = false;
                Debug.Log("[SetInOrderM2] Snapped count turun, reset SetInOrder");
                TaskManagerM2.Instance.ResetTask(TaskType2.SetInOrder);
            }
        }
    }

    private IEnumerator DelayedComplete()
    {
        yield return new WaitForSeconds(setInOrderDelay);

        // Double-check sebelum complete
        var snapPoints = FindObjectsOfType<SnapPointTutorial>();
        int correct = snapPoints.Count(sp => sp.IsCorrectlySnapped);
        if (correct == totalTools && !isCompleted)
        {
            isCompleted = true;
            Debug.Log("[SetInOrderM2] Menandai SetInOrder complete setelah delay");
            TaskManagerM2.Instance.CompleteTask(TaskType2.SetInOrder);
            onAllToolsSnapped.Invoke();
        }

        pendingCompletion = null;
    }
}