using UnityEngine;

public class SnapZoneChecker : MonoBehaviour
{
    public SnappableObjectl1m2 hammer;
    public SnappableObjectl1m2 mop;

    private bool isHammerSnapped = false;
    private bool isMopSnapped = false;

    private void OnEnable()
    {
        if (hammer != null)
        {
            hammer.OnSnapped += OnHammerSnapped;
            hammer.OnReleased += OnHammerReleased;
        }
        if (mop != null)
        {
            mop.OnSnapped += OnMopSnapped;
            mop.OnReleased += OnMopReleased;
        }
    }

    private void OnDisable()
    {
        if (hammer != null)
        {
            hammer.OnSnapped -= OnHammerSnapped;
            hammer.OnReleased -= OnHammerReleased;
        }
        if (mop != null)
        {
            mop.OnSnapped -= OnMopSnapped;
            mop.OnReleased -= OnMopReleased;
        }
    }

    private void OnHammerSnapped()
    {
        isHammerSnapped = true;
        CheckIfAllSnappedAndTasksComplete();
    }

    private void OnHammerReleased()
    {
        isHammerSnapped = false;
    }

    private void OnMopSnapped()
    {
        isMopSnapped = true;
        CheckIfAllSnappedAndTasksComplete();
    }

    private void OnMopReleased()
    {
        isMopSnapped = false;
    }

    private void CheckIfAllSnappedAndTasksComplete()
    {
        if (isHammerSnapped && isMopSnapped)
        {
            if (ShineChecker.Instance.IsAllTasksComplete())
            {
                ShineChecker.Instance.ShowSuccessPanel();
            }
        }
    }
}
