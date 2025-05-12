using BNG;
using UnityEngine;

public class GrabbablePhaseController : MonoBehaviour
{
    [Tooltip("Fase di mana objek ini boleh diambil")]
    public GamePhaseManager.Phase allowedPhase;

    private Grabbable grabbable;
    private Collider col;

    private void Awake()
    {
        grabbable = GetComponent<Grabbable>();
        col = GetComponent<Collider>();
    }

    private void Update()
    {
        if (!GamePhaseManager.Instance) return;

        bool isCorrectPhase = GamePhaseManager.Instance.IsPhase(allowedPhase);

        if (grabbable != null)
        {
            grabbable.enabled = isCorrectPhase;
            Debug.Log($"{gameObject.name} Grabbable aktif: {isCorrectPhase}");
        }

        if (col != null)
        {
            col.enabled = isCorrectPhase;
            Debug.Log($"{gameObject.name} Collider aktif: {isCorrectPhase}");
        }
    }
}

