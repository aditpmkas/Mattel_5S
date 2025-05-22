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

    private bool hammerPlaced = false;
    private bool mopReturned = false; //  Tambahan baru

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

        totalDirt = GameObject.FindGameObjectsWithTag("DirtyFloor").Length;
        Debug.Log($"[ShineProgressTracker] Total kotoran: {totalDirt}");

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

    public void OnHammerSnapped()
    {
        hammerPlaced = true;
        Debug.Log("[ShineProgressTracker] Hammer sudah diletakkan.");
        CheckIfAllClear();
    }

    public void OnHammerReleased()
    {
        hammerPlaced = false;
        Debug.Log("[ShineProgressTracker] Hammer dilepas.");
        if (allCleanedPanel != null)
            allCleanedPanel.SetActive(false);
    }

    public void OnMopReturned()
    {
        mopReturned = true;
        Debug.Log("[ShineProgressTracker] Mop sudah dikembalikan.");
        CheckIfAllClear();
    }

    private void CheckIfAllClear()
    {
        if (cleanedCount >= totalDirt && fixedCracks >= totalCracks && hammerPlaced && mopReturned)
        {
            Debug.Log("[ShineProgressTracker] Semua selesai dan alat dikembalikan!");
            if (allCleanedPanel != null)
                allCleanedPanel.SetActive(true);
        }
        else
        {
            if (allCleanedPanel != null)
                allCleanedPanel.SetActive(false);
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
