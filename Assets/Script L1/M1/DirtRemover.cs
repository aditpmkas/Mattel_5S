using UnityEngine;

public class DirtRemover : MonoBehaviour
{
    [Header("Swipe Settings")]
    public int maxSwipes = 5;

    private int currentSwipes = 0;
    private bool isCleaned = false;

    public void RegisterSwipe()
    {
        // Hanya aktif jika fase saat ini adalah Shine
        if (!GamePhaseManager.Instance.IsPhase(GamePhaseManager.Phase.Shine))
            return;

        // Cegah jika sudah dibersihkan
        if (isCleaned) return;

        currentSwipes++;
        Debug.Log($"[DirtRemover] Swipe ke-{currentSwipes}/{maxSwipes}");

        if (currentSwipes >= maxSwipes)
        {
            isCleaned = true; // tandai sebagai sudah dibersihkan

            Debug.Log("[DirtRemover] Kotoran dibersihkan!");

            // Beri tahu tracker bahwa 1 noda berhasil dibersihkan
            ShineProgressTracker.Instance?.RegisterCleaned();

            // Hancurkan objek ini
            Destroy(gameObject);
        }
    }
}
