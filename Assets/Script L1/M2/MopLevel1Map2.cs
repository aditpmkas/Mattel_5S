using UnityEngine;

public class MopLevel1Map2 : MonoBehaviour
{

    [Header("Objective Tracker")]
    public ShineChecker shineChecker;

    [Header("Audio")]
    [Tooltip("Audio yang akan dimainkan saat mop menyentuh DirtyFloor.")]
    public AudioClip hitDirtyFloorSound;
    [Tooltip("AudioSource yang digunakan untuk memainkan audio. Jika kosong, akan otomatis dibuat.")]
    public AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("DirtyFloor") || other.CompareTag("DirtyFloor"))
        {
            PuddleHealthVR puddleHealth = other.GetComponent<PuddleHealthVR>();
            if (puddleHealth != null)
            {
                puddleHealth.RegisterSwipe(shineChecker);
            }

            // Play sound
            if (hitDirtyFloorSound != null)
            {
                if (audioSource == null)
                {
                    audioSource = GetComponent<AudioSource>();
                    if (audioSource == null)
                    {
                        audioSource = gameObject.AddComponent<AudioSource>();
                    }
                    audioSource.playOnAwake = false;
                    audioSource.spatialBlend = 1f;
                }
                audioSource.clip = hitDirtyFloorSound;
                audioSource.Play();
            }
        }
    }
}