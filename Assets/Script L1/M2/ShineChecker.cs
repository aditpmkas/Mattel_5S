using UnityEngine;
using TMPro;

public class ShineChecker : MonoBehaviour
{
    public static ShineChecker Instance { get; private set; }

    [Header("UI Elements")]
    public TextMeshProUGUI shineCountText;
    public GameObject successPanel;

    private int totalDirt;
    private int cleanedDirt = 0;

    private int totalRootCause;
    private int cleanedRootCause = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        if (successPanel != null)
            successPanel.SetActive(false);

        totalDirt = GameObject.FindGameObjectsWithTag("DirtyFloor").Length;
        totalRootCause = GameObject.FindGameObjectsWithTag("RootCauseLevel1").Length;

        Debug.Log($"[ShineChecker] Total Noda: {totalDirt} | Total Akar Masalah: {totalRootCause}");

        UpdateUIText();
    }

    public void RegisterShineHit()
    {
        if (GameManagerL1M2.Instance.currentPhase != GameManagerL1M2.GamePhase.Shine)
            return;

        cleanedDirt++;
        Debug.Log($"[ShineChecker] Noda Dibersihkan: {cleanedDirt}/{totalDirt}");

        GameManagerL1M2.Instance.RegisterShineHit();
        UpdateUIText();
        CheckSuccess();
    }

    public void RegisterRootCauseHit()
    {
        cleanedRootCause++;
        Debug.Log($"[ShineChecker] Akar Masalah Dibersihkan: {cleanedRootCause}/{totalRootCause}");

        UpdateUIText();
        CheckSuccess();
    }

    private void CheckSuccess()
    {
        if (IsAllTasksComplete())
        {
            Debug.Log("[ShineChecker] Semua tugas selesai! Menunggu hammer dan mop untuk dikembalikan.");
            // Jangan tampilkan success panel di sini.
            // Tunggu SnapZoneChecker untuk konfirmasi alat sudah dikembalikan.
        }
    }

    public bool IsAllTasksComplete()
    {
        return cleanedDirt >= totalDirt && cleanedRootCause >= totalRootCause;
    }

    public void ShowSuccessPanel()
    {
        if (successPanel != null)
        {
            successPanel.SetActive(true);
            Debug.Log("[ShineChecker] Success Panel ditampilkan karena hammer dan mop sudah dikembalikan.");
        }
    }

    private void UpdateUIText()
    {
        if (shineCountText != null)
        {
            shineCountText.text = $"Shine : {cleanedDirt}/{totalDirt}\nAkar Masalah : {cleanedRootCause}/{totalRootCause}";
        }
        else
        {
            Debug.LogWarning("[ShineChecker] shineCountText belum di-assign di Inspector.");
        }
    }
}
