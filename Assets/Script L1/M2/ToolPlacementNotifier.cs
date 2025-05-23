using UnityEngine;

[RequireComponent(typeof(SnappableObjectLevel1Map1))]
public class ToolPlacementNotifier : MonoBehaviour
{
    private SnappableObjectLevel1Map1 snappable;

    public bool isHammer = false;

    void Start()
    {
        snappable = GetComponent<SnappableObjectLevel1Map1>();

        if (snappable != null)
        {
            snappable.OnSnapped += HandleSnapped;
            snappable.OnReleased += HandleReleased;
        }
        else
        {
            Debug.LogError("ToolPlacementNotifier membutuhkan komponen SnappableObjectLevel1Map1!");
        }
    }

    private void HandleSnapped()
    {
        if (ShineProgressTracker.Instance != null)
        {
            if (isHammer)
                ShineProgressTracker.Instance.OnHammerSnapped();
            else
                ShineProgressTracker.Instance.OnMopReturned();
        }
    }

    private void HandleReleased()
    {
        if (ShineProgressTracker.Instance != null && isHammer)
        {
            ShineProgressTracker.Instance.OnHammerReleased();
        }
    }

    private void OnDestroy()
    {
        if (snappable != null)
        {
            snappable.OnSnapped -= HandleSnapped;
            snappable.OnReleased -= HandleReleased;
        }
    }
}
