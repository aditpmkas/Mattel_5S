using UnityEngine;
using TMPro;

public class ShineChecker : MonoBehaviour
{
    [Header("Shine Objective")]
    public int requiredShineCount = 3;
    private int currentShineCount = 0;

    [Header("UI")]
    public TextMeshProUGUI objectiveText;

    private void Start()
    {
        UpdateObjectiveText(); // Tampilkan Shine : 0/3 saat awal
    }

    /// <summary>
    /// Dipanggil ketika pel mengenai objek DirtyFloor
    /// </summary>
    public void RegisterShineHit()
    {
        currentShineCount++;
        Debug.Log("RegisterShineHit dipanggil! Count sekarang: " + currentShineCount);

        if (currentShineCount >= requiredShineCount)
        {
            Debug.Log("Shine Objective Complete!");
            // Tambahkan logic tambahan di sini jika dibutuhkan
        }

        UpdateObjectiveText();
    }

    private void UpdateObjectiveText()
    {
        if (objectiveText != null)
        {
            objectiveText.text = $"Shine : {currentShineCount}/{requiredShineCount}";
        }
        else
        {
            Debug.LogWarning("Objective Text belum di-assign!");
        }
    }
}
