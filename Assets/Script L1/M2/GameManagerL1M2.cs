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

    [Header("Snap Order UI")]
    public int totalSnapPointsToCheck = 6;
    private int snapCorrectCount = 0;
    public TMP_Text setInOrderText;

    [Header("Sorting UI")]
    public int totalSortTargets = 3;
    private int sortDestroyedCount = 0;
    public TMP_Text sortingText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        UpdateSortingUI();
        UpdateSnapUI();
        UpdatePhaseText();

        UpdateGrabbableScripts();  // Set awal sesuai fase
    }

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

    public void CheckAllSnapPoints()
    {
        if (currentPhase != GamePhase.SetInOrder) return;

        SnapPointLevel1[] snapPoints = FindObjectsOfType<SnapPointLevel1>();
        int correctCount = 0;

        foreach (var point in snapPoints)
        {
            if (point.isOccupied && point.snappedObject == point.correctObject)
            {
                correctCount++;
            }
        }

        snapCorrectCount = correctCount;
        UpdateSnapUI();

        if (snapCorrectCount >= totalSnapPointsToCheck)
        {
            Debug.Log("[GameManager] Set In Order selesai!");
            AdvancePhase();
        }
    }

    public void RegisterShineHit()
    {
        if (currentPhase != GamePhase.Shine) return;

        Debug.Log("[GameManager] Shine hit registered");

        // Bisa tambahkan logic lanjutannya di sini jika perlu
    }

    private void UpdateSortingUI()
    {
        if (sortingText != null)
            sortingText.text = $"Sorting : {sortDestroyedCount}/{totalSortTargets}";
    }

    private void UpdateSnapUI()
    {
        if (setInOrderText != null)
            setInOrderText.text = $"Set In Order : {snapCorrectCount}/{totalSnapPointsToCheck}";
    }

    private void UpdatePhaseText()
    {
        if (phaseText != null)
            phaseText.text = $"Phase: {currentPhase}";
    }

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

        UpdatePhaseText();
        UpdateGrabbableScripts(); // Update status grabbable tiap pindah fase
    }

    private void UpdateGrabbableScripts()
    {
        // Matikan semua Grabbable dulu
        var allGrabbables = FindObjectsOfType<Grabbable>();
        foreach (var g in allGrabbables)
        {
            g.enabled = false;
        }

        // Aktifkan Grabbable yang sesuai fase
        switch (currentPhase)
        {
            case GamePhase.Sorting:
                // Aktifkan Grabbable untuk sorting items (misal, tag "Sort")
                EnableGrabbableByTag("Sort");
                break;

            case GamePhase.SetInOrder:
                // Aktifkan Grabbable untuk snap order objects (misal, tag "SnapObject")
                EnableGrabbableByTag("SnapObject");
                break;

            case GamePhase.Shine:
                // Aktifkan Grabbable untuk noda bersih (misal, tag "DirtyFloor")
                EnableGrabbableByTag("DirtyFloor");
                break;
        }
    }

    private void EnableGrabbableByTag(string tag)
    {
        var allGrabbables = FindObjectsOfType<Grabbable>();
        foreach (var g in allGrabbables)
        {
            if (g.gameObject.CompareTag(tag))
            {
                g.enabled = true;
            }
            else
            {
                g.enabled = false;
            }
        }
    }
}
