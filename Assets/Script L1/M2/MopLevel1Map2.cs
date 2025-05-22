using UnityEngine;
using System.Collections.Generic;

public class MopLevel1Map2 : MonoBehaviour
{
    [Header("Objective Tracker")]
    public ShineChecker shineChecker;

    [Header("Audio")]
    public AudioClip hitDirtyFloorSound;
    public AudioSource audioSource;

    // Simpan waktu terakhir swipe per puddle
    private Dictionary<GameObject, float> lastSwipeTimePerPuddle = new Dictionary<GameObject, float>();
    private float swipeCooldown = 0.2f; // cooldown 0.2 detik supaya gak double hit

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("DirtyFloor") || other.CompareTag("DirtyFloor"))
        {
            GameObject puddleObj = other.gameObject;

            float lastTime;
            lastSwipeTimePerPuddle.TryGetValue(puddleObj, out lastTime);
            if (Time.time - lastTime < swipeCooldown)
            {
                // masih cooldown, skip
                return;
            }

            // update waktu terakhir swipe
            lastSwipeTimePerPuddle[puddleObj] = Time.time;

            PuddleHealthVR puddleHealth = other.GetComponent<PuddleHealthVR>();
            if (puddleHealth != null)
            {
                puddleHealth.RegisterSwipe(shineChecker);
            }

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
