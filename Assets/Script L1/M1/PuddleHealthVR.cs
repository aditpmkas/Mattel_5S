using UnityEngine;

public class PuddleHealthVR : MonoBehaviour
{
    [Header("Swipe Settings")]
    public int maxSwipes = 3;

    private int currentSwipes = 0;

    /// <summary>
    /// Dipanggil dari MopLevel1Map1.OnTriggerEnter
    /// </summary>
    public void RegisterSwipe(ShineChecker shineChecker)
    {
        currentSwipes++;
        Debug.Log($"[Puddle] Swipe ke-{currentSwipes}/{maxSwipes}");

        if (currentSwipes >= maxSwipes)
        {
            // 1) Notify ShineChecker
            if (shineChecker != null)
                shineChecker.RegisterShineHit();

            // 2) Hilangkan puddle ini
            Destroy(gameObject);
        }
    }
}
