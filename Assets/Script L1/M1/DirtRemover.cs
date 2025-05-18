using UnityEngine;

public class DirtRemover : MonoBehaviour
{
    [Header("Swipe Settings")]
    public int maxSwipes = 5;

    private int currentSwipes = 0;

    public void RegisterSwipe()
    {
        // Hanya aktif jika fase saat ini adalah Shine
        if (!GamePhaseManager.Instance.IsPhase(GamePhaseManager.Phase.Shine))
            return;

        currentSwipes++;
        Debug.Log($"[DirtRemover] Swipe ke-{currentSwipes}/{maxSwipes}");

        if (currentSwipes >= maxSwipes)
        {
            Debug.Log("[DirtRemover] Kotoran dibersihkan!");

            // Beri tahu tracker bahwa 1 noda berhasil dibersihkan
            ShineProgressTracker.Instance?.RegisterCleaned();

            // Hancurkan objek ini
            Destroy(gameObject);
        }
    }
}
