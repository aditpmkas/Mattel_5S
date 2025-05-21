using System.Linq;
using UnityEngine;
using TMPro;

public class SortingChecker : MonoBehaviour
{
    [Header("Sorting Settings")]
    public GameObject table;
    public int requiredCorrectSorts = 4;
    private int currentCorrectSorts = 0;

    [Header("Bias Zone")]
    public GameObject drawer;
    private int totalBias = 3;
    private int currentBiasCount = 0;

    [Header("UI")]
    public TextMeshProUGUI statusText; // UI gabungan

    private void Start()
    {
        UpdateStatusText();
    }

    private void Update()
    {
        currentBiasCount = CountBiasInDrawer();
        UpdateStatusText();

        bool noSortsLeft = !table.GetComponentsInChildren<Transform>()
                                 .Any(child => child.CompareTag("Sort"));

        if (noSortsLeft && currentCorrectSorts >= requiredCorrectSorts && currentBiasCount >= totalBias)
        {
            Debug.Log("[SortingChecker] Semua tugas Sorting selesai!");
            GamePhaseManager.Instance.SetPhase(GamePhaseManager.Phase.SetInOrder);
            Destroy(this);
        }
    }

    public void IncrementCorrectSort()
    {
        currentCorrectSorts++;
        UpdateStatusText();
    }

    private void UpdateStatusText()
    {
        if (statusText != null)
        {
            statusText.text = $"Sorting : {currentCorrectSorts}/{requiredCorrectSorts}\n" +
                              $"Sorting Bias : {currentBiasCount}/{totalBias}";
        }
    }

    private int CountBiasInDrawer()
    {
        if (drawer == null)
        {
            Debug.LogWarning("[SortingChecker] Drawer belum di-assign!");
            return 0;
        }

        Collider drawerCollider = drawer.GetComponent<Collider>();
        if (drawerCollider == null)
        {
            Debug.LogWarning("[SortingChecker] Drawer tidak memiliki Collider!");
            return 0;
        }

        Collider[] hits = Physics.OverlapBox(
            drawerCollider.bounds.center,
            drawerCollider.bounds.extents,
            drawer.transform.rotation
        );

        return hits.Count(hit =>
            hit.CompareTag("SortBiasA") ||
            hit.CompareTag("SortBiasB") ||
            hit.CompareTag("SortBiasC"));
    }
}
