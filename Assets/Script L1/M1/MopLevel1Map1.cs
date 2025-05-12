using UnityEngine;

public class MopLevel1Map1 : MonoBehaviour
{
    private int cleanedCount = 0;
    public int requiredCleanCount = 3;

    [Header("UI")]
    public GameObject successPanel;

    [Header("Objective Tracker")]
    public ShineChecker shineChecker; // Diisi dari Inspector

    private void Start()
    {
        if (successPanel != null)
            successPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DirtyFloor"))
        {
            Destroy(other.gameObject);
            cleanedCount++;
            Debug.Log($"Pel membersihkan: {other.name} | Total: {cleanedCount}");

            // Beri tahu ShineChecker bahwa ada Shine baru
            if (shineChecker != null)
            {
                shineChecker.RegisterShineHit();
            }
            else
            {
                Debug.LogWarning("ShineChecker belum di-assign ke MopLevel1Map1!");
            }

            // Cek apakah semua sudah dibersihkan
            if (cleanedCount >= requiredCleanCount && successPanel != null)
            {
                successPanel.SetActive(true);
                Debug.Log("Semua kotoran sudah dibersihkan!");
            }
        }
    }
}
