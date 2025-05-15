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
        // Hanya ambil rak yang benar-benar butuh buku (requiredCount > 0)
        var activeShelves = FindObjectsOfType<BookShelfTutorial>()
                              .Where(s => s.requiredCount > 0)
                              .ToList();

        // Jika semua rak aktif sudah complete, beri tahu TaskManager sekali
        if (activeShelves.All(s => s.IsComplete()))
        {
            Debug.Log("[ShelfManager] Semua rak lengkap, complete SetInOrder");
            TaskManager.Instance.CompleteTask(TaskType.SetInOrder);
        }
        else
        {
            // (opsional) debug rak mana yang belum
            foreach (var s in activeShelves.Where(s => !s.IsComplete()))
                Debug.Log($"[ShelfManager] Rak belum selesai: {s.gameObject.name}");
        }
    }
}
