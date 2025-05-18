using UnityEngine;
using BNG;  // Pastikan namespace ini sesuai dengan lokasi Grabbable kamu

public class PhaseControllL1M2 : MonoBehaviour
{
    [Tooltip("Fase di mana objek ini boleh diinteraksi (Grabbable aktif)")]
    public GameManagerL1M2.GamePhase allowedPhase;

    private Grabbable grabbable;
    private Collider col;

    private void Awake()
    {
        grabbable = GetComponent<Grabbable>();
        col = GetComponent<Collider>();
    }

    private void Update()
    {
        if (GameManagerL1M2.Instance == null) return;

        bool isActivePhase = GameManagerL1M2.Instance.currentPhase == allowedPhase;

        if (grabbable != null)
            grabbable.enabled = isActivePhase;

        if (col != null)
            col.enabled = isActivePhase;
    }
}
