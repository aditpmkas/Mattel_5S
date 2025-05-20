using System.Linq;
using UnityEngine;
using TMPro;

public class SortingChecker : MonoBehaviour
{
    [Header("Sorting Settings")]
    public GameObject table; // tempat object Sort & Unsort
    public int requiredCorrectSorts = 4;
    private int currentCorrectSorts = 0;

    [Header("Bias Zone")]
    public GameObject drawer; // zona besar untuk pengecekan SortBias
    private int totalBias = 3;
    private int currentBiasCount = 0;

    [Header("UI")]
    public TextMeshProUGUI objectiveText;
    public TextMeshProUGUI biasText; // UI teks untuk SortBias

    private void Start()
    {
        UpdateObjectiveText();
        UpdateBiasText(currentBiasCount);
    }

    private void Update()
    {
        // Update terus-menerus jumlah bias yang ada di dalam drawer
        currentBiasCount = CountBiasInDrawer();
        UpdateBiasText(currentBiasCount);

        bool noSortsLeft = !table.GetComponentsInChildren<Transform>()
                                 .Any(child => child.CompareTag("Sort"));

        if (noSortsLeft && currentCorrectSorts >= requiredCorrectSorts && currentBiasCount >= totalBias)
        {
            Debug.Log("[SortingChecker] Semua tugas Sorting selesai!");
            GamePhaseManager.Instance.SetPhase(GamePhaseManager.Phase.SetInOrder);
            Destroy(this); // stop pengecekan
        }
    }

    public void IncrementCorrectSort()
    {
        currentCorrectSorts++;
        UpdateObjectiveText();
    }

    private void UpdateObjectiveText()
    {
        if (objectiveText != null)
        {
            objectiveText.text = $"Sorting: {currentCorrectSorts}/{requiredCorrectSorts}";
        }
    }

    private void UpdateBiasText(int count)
    {
        if (biasText != null)
        {
            biasText.text = $"Sorting Bias: {count}/{totalBias}";
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
