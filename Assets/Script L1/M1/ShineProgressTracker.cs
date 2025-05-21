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

    private int totalCracks = 0;
    private int fixedCracks = 0;

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

        // Hitung total noda
        totalDirt = GameObject.FindGameObjectsWithTag("DirtyFloor").Length;
        Debug.Log($"[ShineProgressTracker] Total kotoran: {totalDirt}");

        // Hitung total retakan (Root Cause)
        totalCracks = GameObject.FindGameObjectsWithTag("Crack").Length;
        Debug.Log($"[ShineProgressTracker] Total retakan: {totalCracks}");

        UpdateProgressUI();
    }

    public void RegisterCleaned()
    {
        cleanedCount++;
        Debug.Log($"[ShineProgressTracker] Noda dibersihkan: {cleanedCount}/{totalDirt}");
        UpdateProgressUI();
        CheckIfAllClear();
    }

    public void RegisterCrackFixed()
    {
        fixedCracks++;
        Debug.Log($"[ShineProgressTracker] Retakan diperbaiki: {fixedCracks}/{totalCracks}");
        UpdateProgressUI();
        CheckIfAllClear();
    }

    private void CheckIfAllClear()
    {
        if (cleanedCount >= totalDirt && fixedCracks >= totalCracks)
        {
            Debug.Log("[ShineProgressTracker] Semua kotoran & retakan selesai!");
            if (allCleanedPanel != null)
                allCleanedPanel.SetActive(true);
        }
    }

    private void UpdateProgressUI()
    {
        if (progressText != null)
        {
            progressText.text = $"Shine: {cleanedCount}/{totalDirt}\nRoot Cause Fix: {fixedCracks}/{totalCracks}";
        }
    }
}
