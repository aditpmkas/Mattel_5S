using UnityEngine;
using BNG;

public class PhaseControllL1M2 : MonoBehaviour
{
    [Tooltip("Fase di mana objek ini boleh diinteraksi (Grabbable aktif)")]
    public GameManagerL1M2.GamePhase allowedPhase;

    [Tooltip("Centang jika objek ini adalah bagian dari Shine")]
    public bool isShineObject = false;

    private Grabbable grabbable;
    private Collider col;

    private void Awake()
    {
        grabbable = GetComponent<Grabbable>();
        col = GetComponent<Collider>();
    }

    private void Update()
    {
        if (GameManagerL1M2.Instance == null || ShineChecker.Instance == null) return;

        bool isCorrectPhase = GameManagerL1M2.Instance.currentPhase == allowedPhase;
        bool isRootCauseCleared = ShineChecker.Instance.IsRootCauseComplete();

        // Untuk objek Shine, hanya aktif kalau fase benar dan akar masalah sudah beres
        bool canInteract = isCorrectPhase && (!isShineObject || isRootCauseCleared);

        if (grabbable != null)
            grabbable.enabled = canInteract;

        if (col != null)
            col.enabled = canInteract;
    }
}
