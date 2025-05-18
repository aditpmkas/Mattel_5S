using UnityEngine;
using TMPro;

public class ShineCheckerL1M1 : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI shineCountText;
    public GameObject successPanel;

    private int totalDirt;
    private int cleanedDirt = 0;

    private void Start()
    {
        if (successPanel != null)
            successPanel.SetActive(false);

        // Hitung jumlah noda berdasarkan tag DirtyFloor
        totalDirt = GameObject.FindGameObjectsWithTag("DirtyFloor").Length;
        Debug.Log($"[ShineCheckerL1M1] Jumlah noda: {totalDirt}");

        UpdateShineUIText();
    }

    public void RegisterShineHit()
    {
        // Cek apakah sedang dalam fase Shine
        if (!GamePhaseManager.Instance.IsPhase(GamePhaseManager.Phase.Shine))
            return;

        cleanedDirt++;
        Debug.Log($"[ShineCheckerL1M1] Noda dibersihkan: {cleanedDirt}/{totalDirt}");

        UpdateShineUIText();

        if (cleanedDirt >= totalDirt)
        {
            Debug.Log("[ShineCheckerL1M1] Semua noda dibersihkan!");
            if (successPanel != null)
                successPanel.SetActive(true);

            // Jika ingin pindah fase otomatis setelah Shine selesai, aktifkan baris ini:
            // GamePhaseManager.Instance.SetPhase(GamePhaseManager.Phase.Sorting); // atau fase lain
        }
    }

    private void UpdateShineUIText()
    {
        if (shineCountText != null)
        {
            shineCountText.text = $"Shine : {cleanedDirt}/{totalDirt}";
        }
        else
        {
            Debug.LogWarning("[ShineCheckerL1M1] shineCountText belum di-assign!");
        }
    }
}
