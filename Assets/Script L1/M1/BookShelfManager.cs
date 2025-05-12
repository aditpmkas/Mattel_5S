using UnityEngine;
using TMPro;

public class BookShelfManager : MonoBehaviour
{
    public static BookShelfManager Instance;

    private int correctBooksPlaced = 0;
    private int totalRequiredBooks = 0;

    [Header("UI")]
    public TextMeshProUGUI objectiveText;

    private void Awake()
    {
        Instance = this;

        // Hitung total buku dari semua rak (versi BookShelf)
        BookShelf[] allShelves = FindObjectsOfType<BookShelf>();
        foreach (BookShelf shelf in allShelves)
        {
            totalRequiredBooks += shelf.requiredCount;
        }

        UpdateObjectiveText();
    }

    public void ReportCorrectBookPlaced()
    {
        correctBooksPlaced++;
        UpdateObjectiveText();

        if (correctBooksPlaced >= totalRequiredBooks)
        {
            Debug.Log("Semua buku sudah ditempatkan dengan benar!");
            GamePhaseManager.Instance.SetPhase(GamePhaseManager.Phase.Shine);
        }
    }

    public void ReportCorrectBookRemoved()
    {
        correctBooksPlaced--;
        if (correctBooksPlaced < 0) correctBooksPlaced = 0;
        UpdateObjectiveText();
    }

    private void UpdateObjectiveText()
    {
        if (objectiveText != null)
        {
            objectiveText.text = $"Set In Order: {correctBooksPlaced}/{totalRequiredBooks}";
        }
    }

    // Tambahan untuk mendukung BookShelfTutorial.cs milik temanmu
    public void CheckAllShelvesComplete()
    {
        BookShelfTutorial[] shelves = FindObjectsOfType<BookShelfTutorial>();
        foreach (BookShelfTutorial shelf in shelves)
        {
            if (!shelf.IsComplete())
                return; // Masih ada rak yang belum penuh
        }

        Debug.Log("Semua rak (BookShelfTutorial) sudah lengkap!");
        GamePhaseManager.Instance.SetPhase(GamePhaseManager.Phase.Shine);
    }
}
