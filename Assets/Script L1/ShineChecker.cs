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
        // 1) Reset UI
        if (successPanel != null) successPanel.SetActive(false);

        // 2) Hitung total noda
        totalDirt = GameObject.FindGameObjectsWithTag("DirtyFloor").Length;
        Debug.Log($"[ShineChecker] Jumlah noda: {totalDirt}");
    }

    public void RegisterShineHit()
    {
        cleanedDirt++;
        Debug.Log($"[ShineChecker] Noda dibersihkan: {cleanedDirt}/{totalDirt}");

        UpdateShineUIText();

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
