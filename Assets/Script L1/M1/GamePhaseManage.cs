using UnityEngine;
using TMPro;

public class GamePhaseManager : MonoBehaviour
{
    public enum Phase
    {
        Sorting,
        SetInOrder,
        Shine
    }

    public static GamePhaseManager Instance;

    [SerializeField] private TextMeshProUGUI phaseText;

    public Phase currentPhase;

    private void Awake()
    {
        Instance = this;
        currentPhase = Phase.Sorting;  // Default phase
        UpdatePhaseText();
    }

    public void SetPhase(Phase phase)
    {
        currentPhase = phase;
        Debug.Log("Fase berubah menjadi: " + currentPhase);
        UpdatePhaseText();
    }

    public bool IsPhase(Phase phase)
    {
        return currentPhase == phase;
    }

    public Phase GetCurrentPhase()
    {
        return currentPhase;
    }

    private void UpdatePhaseText()
    {
        if (phaseText != null)
        {
            phaseText.text = "Phase: " + currentPhase.ToString();
        }
    }
}
