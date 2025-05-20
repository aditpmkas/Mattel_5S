using System.Collections;
using System.Linq;
using UnityEngine;

public class ShelfManager : MonoBehaviour
{
    public static ShelfManager Instance;

    [Header("Delay Settings")]
    [Tooltip("Delay in seconds before marking SetInOrder complete")]
    public float setInOrderDelay = 1f;

    // Coroutine handle for pending completion
    private Coroutine pendingCompletion;

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Dipanggil setiap kali sebuah rak berubah status
    /// </summary>
    public void NotifyBookPlaced()
    {
        CheckAllShelvesComplete();
    }

    private void CheckAllShelvesComplete()
    {
        // Ambil rak yang butuh buku
        var activeShelves = FindObjectsOfType<BookShelfTutorial>()
                             .Where(s => s.requiredCount > 0)
                             .ToList();

        bool allComplete = activeShelves.All(s => s.IsComplete());
        var tm = TaskManager.Instance;

        if (allComplete)
        {
            // Jika belum pernah complete dan belum ada pending coroutine, jadwalkan complete dengan delay
            if (!tm.IsTaskDone(TaskType.SetInOrder) && pendingCompletion == null)
            {
                Debug.Log($"[ShelfManager] Semua rak lengkap, akan complete SetInOrder dalam {setInOrderDelay} detik");
                pendingCompletion = StartCoroutine(DelayedCompleteSetInOrder());
            }
        }
        else
        {
            // Jika ada pending complete, batalkan
            if (pendingCompletion != null)
            {
                Debug.Log("[ShelfManager] Pembatalan pending completion SetInOrder karena rak belum lengkap");
                StopCoroutine(pendingCompletion);
                pendingCompletion = null;
            }

            // Jika sebelumnya sudah complete tapi sekarang ada yang belum
            if (tm.IsTaskDone(TaskType.SetInOrder))
            {
                Debug.Log("[ShelfManager] Rak kembali belum lengkap, reset SetInOrder");
                tm.ResetTask(TaskType.SetInOrder);
            }

            // Debug rak mana yang belum
            foreach (var s in activeShelves.Where(s => !s.IsComplete()))
                Debug.Log($"[ShelfManager] Rak belum selesai: {s.gameObject.name}");
        }
    }

    private IEnumerator DelayedCompleteSetInOrder()
    {
        yield return new WaitForSeconds(setInOrderDelay);

        var tm = TaskManager.Instance;
        if (!tm.IsTaskDone(TaskType.SetInOrder))
        {
            Debug.Log("[ShelfManager] Menandai SetInOrder sebagai complete setelah delay");
            tm.CompleteTask(TaskType.SetInOrder);
        }

        pendingCompletion = null;
    }
}
