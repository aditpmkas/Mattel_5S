using BNG;
using UnityEngine.UI;
using UnityEngine;

public class CrackFLevel1Map1 : MonoBehaviour
{
    [Header("Drip Particle")]
    public ParticleSystem dripPS;

    public Grabbable hammer;
    public Canvas targetCanvas;
    private GraphicRaycaster raycaster;

    // Audio untuk crack fixed
    public AudioSource audioSource;
    public AudioClip fixSound;

    private Collider _buttonCollider;
    private bool isFixed = false;

    private void Start()
    {
        if (targetCanvas != null)
        {
            raycaster = targetCanvas.GetComponent<GraphicRaycaster>();
            raycaster.enabled = false; // Disable at start
        }

        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        // Disable collider semua puddle sampai retakan diperbaiki
        foreach (var p in GameObject.FindGameObjectsWithTag("DirtyFloor"))
        {
            var col = p.GetComponent<Collider>();
            if (col != null) col.enabled = false;
        }
    }

    void Update()
    {
        if (hammer != null && raycaster != null)
        {
            raycaster.enabled = hammer.BeingHeld;
        }
    }

    public void FixCrack()
    {
        if (isFixed) return;
        isFixed = true;

        // Stop particle tetesan
        if (dripPS != null) dripPS.Stop();

        // Enable collider semua puddle
        foreach (var p in GameObject.FindGameObjectsWithTag("DirtyFloor"))
        {
            var col = p.GetComponent<Collider>();
            if (col != null) col.enabled = true;
        }

        // Notifikasi ke ShineProgressTracker
        if (ShineProgressTracker.Instance != null)
            ShineProgressTracker.Instance.RegisterCrackFixed();
        else
            Debug.LogWarning("ShineProgressTracker.Instance null!");

        Debug.Log("Crack fixed! Puddles now unlocked.");

        // Putar suara fix crack, setelah itu destroy
        if (audioSource != null && fixSound != null)
        {
            audioSource.PlayOneShot(fixSound);
            // Delay destroy agar suara sempat diputar
            Destroy(gameObject, fixSound.length);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
