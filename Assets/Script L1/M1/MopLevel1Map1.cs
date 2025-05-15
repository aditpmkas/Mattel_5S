using UnityEngine;

public class MopLevel1Map1 : MonoBehaviour
{
    [Header("UI")]
    public GameObject successPanel;

    [Header("Objective Tracker")]
    public ShineChecker shineChecker;

    private void Start()
    {
        if (successPanel != null)
        {
            successPanel.SetActive(false);
            PuddleHealthVR.successPanel = successPanel; // Set untuk static panel
        }

        PuddleHealthVR.ResetCounter(); // Reset awal
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DirtyFloor"))
        {
            PuddleHealthVR puddleHealth = other.GetComponent<PuddleHealthVR>();
            if (puddleHealth != null)
            {
                puddleHealth.RegisterSwipe(shineChecker);
            }
        }
    }
}
