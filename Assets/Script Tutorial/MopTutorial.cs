using UnityEngine;

public class MopTutorial : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("DirtyFloor") || other.CompareTag("DirtyFloor"))
        {
            PuddleHealthT puddleHealth = other.GetComponent<PuddleHealthT>();

            if (puddleHealth != null)
            {
                puddleHealth.AddSwipe();
            }
        }
    }
}
