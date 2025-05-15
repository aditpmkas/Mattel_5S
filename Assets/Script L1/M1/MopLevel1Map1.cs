using UnityEngine;

public class MopLevel1Map1 : MonoBehaviour
{

    [Header("Objective Tracker")]
    public ShineChecker shineChecker;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("DirtyFloor") || other.CompareTag("DirtyFloor"))
        {
            PuddleHealthVR puddleHealth = other.GetComponent<PuddleHealthVR>();
            if (puddleHealth != null)
            {
                puddleHealth.RegisterSwipe(shineChecker);
            }
        }
    }
}
