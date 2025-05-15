using UnityEngine;

public class PuddleHealthVR : MonoBehaviour
{
    public int maxSwipes = 3;
    private int currentSwipes = 0;

    public static int totalPuddles = 0;
    public static int destroyedPuddles = 0;

    public static GameObject successPanel; // Di-set sekali dari luar

    private void Start()
    {
        totalPuddles++;
    }

    public void RegisterSwipe(ShineChecker shineChecker)
    {
        currentSwipes++;
        Debug.Log($"Noda diswipe {currentSwipes}/{maxSwipes}");

        if (currentSwipes >= maxSwipes)
        {
            destroyedPuddles++;
            Debug.Log($"Noda dihapus ({destroyedPuddles}/{totalPuddles})");

            if (shineChecker != null)
                shineChecker.RegisterShineHit();

            if (destroyedPuddles >= totalPuddles)
            {
                Debug.Log("Semua noda sudah dibersihkan!");
                if (successPanel != null)
                    successPanel.SetActive(true);
                else
                    Debug.LogWarning("successPanel belum di-assign ke PuddleHealthVR!");
            }

            Destroy(gameObject);
        }
    }

    public static void ResetCounter()
    {
        totalPuddles = 0;
        destroyedPuddles = 0;
    }
}
