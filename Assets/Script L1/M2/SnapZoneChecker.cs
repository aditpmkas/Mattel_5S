using UnityEngine;

public class SnapZoneChecker : MonoBehaviour
{
    public SnappableObjectl1m2 hammer;
    public SnappableObjectl1m2 mop;

    private void Update()
    {
        if (hammer != null && mop != null)
        {
            bool hammerSnapped = hammer.IsFullySnapped();
            bool mopSnapped = mop.IsFullySnapped();
            bool taskComplete = ShineChecker.Instance != null && ShineChecker.Instance.IsAllTasksComplete();

            if (hammerSnapped && mopSnapped && taskComplete)
            {
                ShineChecker.Instance.ShowSuccessPanel();
            }
        }
    }
}
