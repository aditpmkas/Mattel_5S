using BNG;
using UnityEngine;
using UnityEngine.UI;

public class CrackFLevel1Map2 : MonoBehaviour
{
    [Header("Drip Particle")]
    public ParticleSystem dripPS;

    public Grabbable hammer;
    public Canvas targetCanvas;
    private GraphicRaycaster raycaster;

    private bool isFixed = false;

    [Header("Sound")]
    public AudioClip fixSound;
    private AudioSource audioSource;

    private void Start()
    {
        // Inisialisasi raycaster jika ada canvas
        if (targetCanvas != null)
        {
            raycaster = targetCanvas.GetComponent<GraphicRaycaster>();
            raycaster.enabled = false;
        }

        // Tambahkan atau ambil AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Disable semua collider puddle di awal
        foreach (var puddle in GameObject.FindGameObjectsWithTag("DirtyFloor"))
        {
            Collider col = puddle.GetComponent<Collider>();
            if (col != null) col.enabled = false;
        }
    }

    private void Update()
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

        // 1. Hentikan partikel tetesan
        if (dripPS != null) dripPS.Stop();

        // 2. Aktifkan semua collider puddle
        foreach (var puddle in GameObject.FindGameObjectsWithTag("DirtyFloor"))
        {
            Collider col = puddle.GetComponent<Collider>();
            if (col != null) col.enabled = true;
        }

        // 3. Notifikasi ke ShineProgressTracker
        if (ShineProgressTracker.Instance != null)
            ShineProgressTracker.Instance.RegisterCrackFixed();
        else
            Debug.LogWarning("ShineProgressTracker.Instance null!");

        // 4. Tambahkan ke ShineChecker
        if (ShineChecker.Instance != null)
            ShineChecker.Instance.RegisterRootCauseHit();
        else
            Debug.LogWarning("ShineChecker.Instance null!");

        // 5. Mainkan suara perbaikan
        if (fixSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(fixSound);
            Destroy(gameObject, fixSound.length); // Hancurkan setelah audio selesai
        }
        else
        {
            Destroy(gameObject); // Hancurkan langsung jika tidak ada suara
        }

        Debug.Log("Crack fixed! Puddles now unlocked.");
    }
}
