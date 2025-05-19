using UnityEngine;

public class MopTutorial : MonoBehaviour
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
            PuddleHealthT puddleHealth = other.GetComponent<PuddleHealthT>();
            if (puddleHealth != null)
            {
                puddleHealth.AddSwipe();
            }
        }
    }
}
