using System.Linq;
using UnityEngine;
using TMPro;

public class SortingChecker : MonoBehaviour
{
    public GameObject table; // tempat object Sort & Unsort
    public int requiredCorrectSorts = 4;
    private int currentCorrectSorts = 0;

    [Header("UI")]
    public TextMeshProUGUI objectiveText; //  assign ini di Inspector

    private void Start()
    {
        UpdateObjectiveText(); // tampilkan 0/4 di awal
    }

    private void Update()
    {
        bool noSortsLeft = !table.GetComponentsInChildren<Transform>()
                                 .Any(child => child.CompareTag("Sort"));

        if (noSortsLeft && currentCorrectSorts >= requiredCorrectSorts)
        {
            GamePhaseManager.Instance.SetPhase(GamePhaseManager.Phase.SetInOrder);
            Destroy(this); // tidak perlu dicek terus
        }
    }

    public void IncrementCorrectSort()
    {
        currentCorrectSorts++;
        UpdateObjectiveText(); // update tampilan UI
    }

    private void UpdateObjectiveText()
    {
        if (objectiveText != null)
        {
            objectiveText.text = $"Objective: {currentCorrectSorts}/{requiredCorrectSorts}";
        }
    }
}
