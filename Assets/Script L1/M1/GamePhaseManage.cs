using UnityEngine;

public class GamePhaseManager : MonoBehaviour
{
    public enum Phase
    {
        Sorting,
        SetInOrder,
        Shine
    }

    public static GamePhaseManager Instance;

    private Phase currentPhase;

    private void Awake()
    {
        Instance = this;
        currentPhase = Phase.Sorting;  // Default phase, bisa disesuaikan
    }

    public void SetPhase(Phase phase)
    {
        currentPhase = phase;
        Debug.Log("Fase berubah menjadi: " + currentPhase);
    }

    public bool IsPhase(Phase phase)
    {
        return currentPhase == phase;
    }

    public Phase GetCurrentPhase()
    {
        return currentPhase;
    }
}
