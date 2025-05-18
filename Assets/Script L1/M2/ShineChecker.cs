using UnityEngine;
using TMPro;

public class ShineChecker : MonoBehaviour
{
    public TextMeshProUGUI shineCountText;
    public GameObject successPanel;

    private int totalDirt;
    private int cleanedDirt = 0;

    private void Start()
    {
        if (successPanel != null) successPanel.SetActive(false);

        // Hitung noda saat fase Shine mulai
        totalDirt = GameObject.FindGameObjectsWithTag("DirtyFloor").Length;
        Debug.Log($"[ShineChecker] Jumlah noda: {totalDirt}");

        UpdateShineUIText();
    }

    public void RegisterShineHit()
    {
        // Cek apakah game sedang di fase Shine
        if (GameManagerL1M2.Instance.currentPhase != GameManagerL1M2.GamePhase.Shine)
            return;

        cleanedDirt++;
        Debug.Log($"[ShineChecker] Noda dibersihkan: {cleanedDirt}/{totalDirt}");

        UpdateShineUIText();

        // Update juga ke GameManager untuk pengelolaan fase
        GameManagerL1M2.Instance.RegisterShineHit();

        if (cleanedDirt >= totalDirt)
        {
            Debug.Log("[ShineChecker] Semua noda dibersihkan!");
            if (successPanel != null)
                successPanel.SetActive(true);
        }
    }

    private void UpdateShineUIText()
    {
        if (shineCountText != null)
            shineCountText.text = $"Shine : {cleanedDirt}/{totalDirt}";
        else
            Debug.LogWarning("[ShineChecker] shineCountText belum di-assign!");
    }
}
