using UnityEngine;
using BNG;

public class ShineTutorial : MonoBehaviour
{
    public static ShineTutorial Instance;
    private bool crackIsFixed = false;

    [Header("Return Zone Settings")]
    [Tooltip("Pre-placed ReturnMopZone GameObject (disabled at start)")]
    public GameObject returnZone;

    private int totalDirt;
    private int cleanedDirt;
    private bool returnZoneEnabled = false;

    public GameObject returnZoneTrigger;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        // Hitung total tapi nanti hanya di-enable setelah crack fixed
        totalDirt = GameObject.FindGameObjectsWithTag("DirtyFloor").Length;

        if (returnZoneTrigger == null)
            Debug.LogError("ReturnZoneTrigger belum di-assign!");

        // Matiin hanya trigger-nya
        var col = returnZoneTrigger.GetComponent<Collider>();
        if (col) col.enabled = false;

        var rz = returnZoneTrigger.GetComponent<ReturnMopZone>();
        if (rz) rz.enabled = false;
    }
    public void CrackFixed()
    {
        crackIsFixed = true;
        Debug.Log("[ShineTutorial] Crack has been fixed.");
        // (Opsional) jalankan animasi atau sound feedback di sini
    }

    /// <summary>
    /// Call this each time a puddle is cleaned.
    /// </summary>
    public void DirtCleaned()
    {
        if (!crackIsFixed)
        {
            Debug.LogWarning("Cannot clean dirt before fixing crack!");
            return;
        }

        cleanedDirt++;
        Debug.Log($"[ShineTutorial] Dirt cleaned: {cleanedDirt}/{totalDirt}");

        // Enable return zone only after all dirt is cleaned
        if (cleanedDirt >= totalDirt && !returnZoneEnabled)
        {
            // Hidupkan kembali trigger-nya
            var col = returnZoneTrigger.GetComponent<Collider>();
            if (col) col.enabled = true;

            var rz = returnZoneTrigger.GetComponent<ReturnMopZone>();
            if (rz) rz.enabled = true;

            returnZoneEnabled = true;
            Debug.Log("[ShineTutorial] All puddles cleaned — ReturnZoneTrigger aktif.");
        }
    }
}