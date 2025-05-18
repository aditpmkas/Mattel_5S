using UnityEngine;
using TMPro;
using BNG;

public class GameManagerL1M2 : MonoBehaviour
{
    public static GameManagerL1M2 Instance;

    public enum GamePhase
    {
        Sorting,
        SetInOrder,
        Shine
    }

    [Header("Phase Control")]
    public GamePhase currentPhase = GamePhase.Sorting;
    public TMP_Text phaseText;

    [Header("Sorting UI")]
    public int totalSortTargets = 3;
    private int sortDestroyedCount = 0;
    public TMP_Text sortingText;

    [Header("Set In Order UI")]
    public int totalSnapPointsToCheck = 6;
    private int snapCorrectCount = 0;
    public TMP_Text setInOrderText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        UpdateAllUI();
        UpdateGrabbableScripts(); // Aktifkan sesuai fase awal
    }

    // ======================= Sorting =======================

    public void IncrementSortDestroyed()
    {
        if (currentPhase != GamePhase.Sorting) return;

        sortDestroyedCount++;
        UpdateSortingUI();

        if (sortDestroyedCount >= totalSortTargets)
        {
            Debug.Log("[GameManager] Sorting selesai!");
            AdvancePhase();
        }
    }

    private void UpdateSortingUI()
    {
        if (sortingText != null)
            sortingText.text = $"Sorting : {sortDestroyedCount}/{totalSortTargets}";
    }

    // =================== Set In Order ======================

    public void CheckAllSnapPoints()
    {
        if (currentPhase != GamePhase.SetInOrder) return;

        SnapPointLevel1[] snapPoints = FindObjectsOfType<SnapPointLevel1>();
        int correctCount = 0;

        foreach (var point in snapPoints)
        {
            if (point.isOccupied && point.snappedObject == point.correctObject)
                correctCount++;
        }

        snapCorrectCount = correctCount;
        UpdateSnapUI();

        if (snapCorrectCount >= totalSnapPointsToCheck)
        {
            Debug.Log("[GameManager] Set In Order selesai!");
            AdvancePhase();
        }
    }

    private void UpdateSnapUI()
    {
        if (setInOrderText != null)
            setInOrderText.text = $"Set In Order : {snapCorrectCount}/{totalSnapPointsToCheck}";
    }

    // ======================== Shine ========================

    public void RegisterShineHit()
    {
        if (currentPhase != GamePhase.Shine) return;

        Debug.Log("[GameManager] Shine hit registered");
        // Tambahkan logika shine jika diperlukan
    }

    // =================== Phase Management ===================

    private void AdvancePhase()
    {
        switch (currentPhase)
        {
            case GamePhase.Sorting:
                currentPhase = GamePhase.SetInOrder;
                break;
            case GamePhase.SetInOrder:
                currentPhase = GamePhase.Shine;
                break;
            case GamePhase.Shine:
                Debug.Log("[GameManager] Semua fase selesai!");
                break;
        }

        UpdateAllUI();
        UpdateGrabbableScripts();
    }

    private void UpdateAllUI()
    {
        UpdateSortingUI();
        UpdateSnapUI();
        UpdatePhaseText();
    }

    private void UpdatePhaseText()
    {
        if (phaseText != null)
            phaseText.text = $"Phase: {FormatPhaseName(currentPhase)}";
    }

    private string FormatPhaseName(GamePhase phase)
    {
        switch (phase)
        {
            case GamePhase.Sorting: return "Sorting";
            case GamePhase.SetInOrder: return "Set In Order";
            case GamePhase.Shine: return "Shine";
            default: return phase.ToString();
        }
    }

    // ================= Grabbable Control ==================

    private void UpdateGrabbableScripts()
    {
        var allGrabbables = FindObjectsOfType<Grabbable>();

        // Matikan semua
        foreach (var g in allGrabbables)
            g.enabled = false;

        // Nyalakan yang sesuai tag fase
        switch (currentPhase)
        {
            case GamePhase.Sorting:
                EnableGrabbablesWithTag("Sort");
                break;
            case GamePhase.SetInOrder:
                EnableGrabbablesWithTag("SnapObject");
                break;
            case GamePhase.Shine:
                EnableGrabbablesWithTag("DirtyFloor");
                break;
        }
    }

    private void EnableGrabbablesWithTag(string tag)
    {
        var allGrabbables = FindObjectsOfType<Grabbable>();

        foreach (var g in allGrabbables)
        {
            g.enabled = g.gameObject.CompareTag(tag);
        }
    }
}
