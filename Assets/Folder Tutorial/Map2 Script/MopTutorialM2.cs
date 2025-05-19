using UnityEngine;

public class MopTutorialM2 : MonoBehaviour
{
    public AudioSource mopSound; // Assign via Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("DirtyFloor") || other.CompareTag("DirtyFloor"))
        {
            // Play sound
            if (mopSound != null && !mopSound.isPlaying)
            {
                mopSound.Play();
            }

            // Apply mop effect
            PuddleHealthM2 puddleHealth = other.GetComponent<PuddleHealthM2>();
            if (puddleHealth != null)
            {
                puddleHealth.AddSwipe();
            }
        }
    }
}
