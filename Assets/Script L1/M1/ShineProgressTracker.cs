using UnityEngine;
using TMPro;

public class ShineProgressTracker : MonoBehaviour
{
    public static ShineProgressTracker Instance;

    [Header("UI")]
    public TextMeshProUGUI progressText;
    public GameObject allCleanedPanel;

    private int totalDirt = 0;
    private int cleanedCount = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        if (allCleanedPanel != null)
            allCleanedPanel.SetActive(false);

        // Hitung semua objek dengan tag DirtyFloor
        totalDirt = GameObject.FindGameObjectsWithTag("DirtyFloor").Length;
        Debug.Log($"[ShineProgressTracker] Total kotoran: {totalDirt}");

        UpdateProgressUI();
    }

    public void RegisterCleaned()
    {
        cleanedCount++;
        Debug.Log($"[ShineProgressTracker] Noda dibersihkan: {cleanedCount}/{totalDirt}");

        UpdateProgressUI();

        if (cleanedCount >= totalDirt)
        {
            Debug.Log("[ShineProgressTracker] Semua noda bersih!");
            if (allCleanedPanel != null)
                allCleanedPanel.SetActive(true);

            // Kamu bisa tambahkan logic pindah fase otomatis di sini jika mau
            // GamePhaseManager.Instance.SetPhase(GamePhaseManager.Phase.Sorting); 
        }
    }

    private void UpdateProgressUI()
    {
        if (progressText != null)
            progressText.text = $"Shine: {cleanedCount}/{totalDirt}";
    }
}
