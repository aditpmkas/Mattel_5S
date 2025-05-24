using BNG;
using UnityEngine;

public class GrabbablePhaseController : MonoBehaviour
{
    [Tooltip("Fase di mana objek ini boleh diambil")]
    public GamePhaseManager.Phase allowedPhase;

    [Tooltip("Ceklis jika object ini termasuk akar masalah (harus diselesaikan dulu)")]
    public bool isRootCause = false;

    private Grabbable grabbable;
    private Collider col;

    private void Awake()
    {
        grabbable = GetComponent<Grabbable>();
        col = GetComponent<Collider>();
    }

    private void Update()
    {
        if (!GamePhaseManager.Instance || ShineProgressTracker.Instance == null) return;

        bool isCorrectPhase = GamePhaseManager.Instance.IsPhase(allowedPhase);

        bool canBeInteracted;

        if (isRootCause)
        {
            // Objek akar masalah hanya aktif di phase yang diizinkan
            canBeInteracted = isCorrectPhase;
        }
        else
        {
            // Objek non-akar masalah:
            // - di phase Sorting atau SetInOrder boleh interaksi tanpa harus tunggu root cause selesai
            // - di phase Shine harus tunggu root cause selesai dulu
            if (allowedPhase == GamePhaseManager.Phase.Sorting || allowedPhase == GamePhaseManager.Phase.SetInOrder)
                canBeInteracted = isCorrectPhase;
            else
                canBeInteracted = isCorrectPhase && ShineProgressTracker.Instance.IsRootCauseComplete();
        }

        if (grabbable != null)
            grabbable.enabled = canBeInteracted;

        if (col != null)
            col.enabled = canBeInteracted;

        Debug.Log($"{gameObject.name} Interact aktif: {canBeInteracted} (isRootCause: {isRootCause})");
    }
}
