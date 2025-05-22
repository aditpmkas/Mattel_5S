using System.Linq;
using UnityEngine;
using System.Collections;

public class SortingTutorialM2 : MonoBehaviour
{
    public GameObject table_; // tempat objek Sort & Unsort
    public GameObject trash;  // tempat trigger Trash (DestroyTutorial)

    [Header("Sorting Requirements")]
    public int requiredCorrectSorts;
    private int currentCorrectSorts;

    [Header("Bias Requirements")]
    public int requiredBiasCount;
    private int currentBiasCount;

    [Header("Completion Delay")]
    [Tooltip("Waktu jeda sebelum menandai task selesai (detik)")]
    public float completionDelay = 2f;

    // track state
    private bool taskCompleted = false;
    private bool isCompleting = false; // mencegah multiple coroutine

    private void Start()
    {
        // Log status awal sekali saja
        Debug.Log($"[SortingTutorial] Correct: {currentCorrectSorts}/{requiredCorrectSorts}, " +
                  $"Bias: {currentBiasCount}/{requiredBiasCount}");
    }

    private void Update()
    {
        // hanya cek reset di update (jika count turun)
        if (taskCompleted)
            CheckReset();
    }

    public void IncrementCorrectSort()
    {
        currentCorrectSorts++;
        Debug.Log($"[SortingTutorial] Correct: {currentCorrectSorts}/{requiredCorrectSorts}");
        CheckCompletion();
    }

    public void IncrementBiasCount()
    {
        currentBiasCount++;
        Debug.Log($"[SortingTutorial] Bias: {currentBiasCount}/{requiredBiasCount}");
        CheckCompletion();
    }

    public void DecrementBiasCount()
    {
        currentBiasCount--;
        if (currentBiasCount < 0) currentBiasCount = 0;
        Debug.Log($"[SortingTutorial] Bias: {currentBiasCount}/{requiredBiasCount}");
        CheckReset();
    }

    private void CheckCompletion()
    {
        if (taskCompleted || isCompleting)
            return;

        bool noSortsLeft = !table_.GetComponentsInChildren<Transform>()
                             .Any(child => child.CompareTag("Sort"));

        if (noSortsLeft
            && currentCorrectSorts >= requiredCorrectSorts
            && currentBiasCount >= requiredBiasCount)
        {
            // mulai coroutine dengan delay
            StartCoroutine(DelayedComplete());
        }
    }

    private IEnumerator DelayedComplete()
    {
        isCompleting = true;
        Debug.Log($"[SortingTutorial] Conditions met. Completing in {completionDelay} seconds...");
        yield return new WaitForSeconds(completionDelay);

        // Pastikan kondisi masih terpenuhi
        bool noSortsLeft = !table_.GetComponentsInChildren<Transform>()
                             .Any(child => child.CompareTag("Sort"));
        if (noSortsLeft
            && currentCorrectSorts >= requiredCorrectSorts
            && currentBiasCount >= requiredBiasCount)
        {
            Debug.Log("[SortingTutorial] Sorting + Bias task completed!");
            TaskManagerM2.Instance.CompleteTask(TaskType2.Sorting);
            taskCompleted = true;
        }
        else
        {
            Debug.Log("[SortingTutorial] Conditions changed during delay; completion cancelled.");
        }

        isCompleting = false;
    }

    private void CheckReset()
    {
        bool noSortsLeft = !table_.GetComponentsInChildren<Transform>()
                             .Any(child => child.CompareTag("Sort"));
        bool allConditions = noSortsLeft
                             && currentCorrectSorts >= requiredCorrectSorts
                             && currentBiasCount >= requiredBiasCount;

        if (!allConditions && taskCompleted)
        {
            Debug.Log("[SortingTutorial] Conditions no longer met, resetting task.");
            TaskManagerM2.Instance.ResetTask(TaskType2.Sorting);
            taskCompleted = false;
        }
    }
}
