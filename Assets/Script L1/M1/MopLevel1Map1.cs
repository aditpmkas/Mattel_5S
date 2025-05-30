using UnityEngine;

public class MopLevel1Map1 : MonoBehaviour
{
    [Header("Audio")]
    [Tooltip("Audio yang akan dimainkan saat mop menyentuh DirtyFloor.")]
    public AudioClip hitDirtyFloorSound;

    [Tooltip("AudioSource yang digunakan untuk memainkan audio. Jika kosong, akan otomatis dibuat.")]
    public AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        // Cek jika yang disentuh adalah layer atau tag DirtyFloor
        if (other.gameObject.layer == LayerMask.NameToLayer("DirtyFloor") || other.CompareTag("DirtyFloor"))
        {
            DirtRemover dirt = other.GetComponent<DirtRemover>();
            if (dirt != null)
            {
                dirt.RegisterSwipe();
            }

            // Play sound jika tersedia
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
