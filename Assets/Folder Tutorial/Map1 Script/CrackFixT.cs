using UnityEngine;

public class CrackFixT : MonoBehaviour
{
    [Header("Drip Particle")]
    [Tooltip("ParticleSystem untuk tetesan air")]
    public ParticleSystem dripPS;

    private bool isFixed = false;

    private void Start()
    {
        // Disable collider semua puddle sampai retakan diperbaiki
        foreach (var p in GameObject.FindGameObjectsWithTag("DirtyFloor"))
        {
            var col = p.GetComponent<Collider>();
            if (col != null) col.enabled = false;
        }
    }

    // Method dipanggil langsung oleh Button OnClick()
    public void FixCrack()
    {
        if (isFixed) return;
        isFixed = true;

        // 1) Stop particle tetesan
        if (dripPS != null) dripPS.Stop();

        // 2) (Opsional) hilangkan mesh/visual crack
        // gameObject.SetActive(false);

        // 3) Enable collider semua puddle
        foreach (var p in GameObject.FindGameObjectsWithTag("DirtyFloor"))
        {
            var col = p.GetComponent<Collider>();
            if (col != null) col.enabled = true;
        }

        // 4) Notifikasi ke ShineTutorial
        if (ShineTutorial.Instance != null)
            ShineTutorial.Instance.CrackFixed();
        else
            Debug.LogWarning("ShineTutorial.Instance null!");

        Debug.Log("Crack fixed! Puddles now unlocked.");
    }
}
