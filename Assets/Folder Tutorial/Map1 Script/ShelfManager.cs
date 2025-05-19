using System.Linq;
using UnityEngine;

public class ShelfManager : MonoBehaviour
{
    public static ShelfManager Instance;

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
            // Jika belum pernah complete, tandai complete
            if (!tm.IsTaskDone(TaskType.SetInOrder))
            {
                Debug.Log("[ShelfManager] Semua rak lengkap, complete SetInOrder");
                tm.CompleteTask(TaskType.SetInOrder);
            }
        }
        else
        {
            // Jika sebelumnya sudah complete tapi sekarang ada yang belum
            if (tm.IsTaskDone(TaskType.SetInOrder))
            {
                Debug.Log("[ShelfManager] Rak kembali belum lengkap, reset SetInOrder");
                tm.ResetTask(TaskType.SetInOrder);
            }

            // (opsional) debug rak mana yang belum
            foreach (var s in activeShelves.Where(s => !s.IsComplete()))
                Debug.Log($"[ShelfManager] Rak belum selesai: {s.gameObject.name}");
        }
    }

}
