using UnityEngine;
using TMPro;

public class ShineProgressTracker : MonoBehaviour
{
    public static ShineProgressTracker Instance;

    [Header("UI")]
    public TextMeshProUGUI progressText;
    public GameObject allCleanedPanel;

    [Header("Audio")]
    public AudioSource completeSound;

    private int totalDirt = 0;
    private int cleanedCount = 0;

    private int totalCracks = 0;
    private int fixedCracks = 0;

    private bool hammerPlaced = false;
    private bool mopReturned = false;

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

    public void OnMopReleased()
    {
        mopReturned = false;
        Debug.Log("[ShineProgressTracker] Mop dilepas.");
        if (allCleanedPanel != null)
            allCleanedPanel.SetActive(false);
    }

    private void CheckIfAllClear()
    {
        if (cleanedCount >= totalDirt && fixedCracks >= totalCracks && hammerPlaced && mopReturned)
        {
            Debug.Log("[ShineProgressTracker] Semua selesai dan alat dikembalikan!");

            if (allCleanedPanel != null)
                allCleanedPanel.SetActive(true);

            if (completeSound != null && !completeSound.isPlaying)
                completeSound.Play();
        }
        else
        {
            if (allCleanedPanel != null)
                allCleanedPanel.SetActive(false);
        }
    }

    public bool IsRootCauseComplete()
    {
        return fixedCracks >= totalCracks;
    }

    private void UpdateProgressUI()
    {
        if (progressText != null)
        {
            progressText.text = $"Sumber Masalah: {fixedCracks}/{totalCracks}\nShine: {cleanedCount}/{totalDirt}";
        }
    }
}
