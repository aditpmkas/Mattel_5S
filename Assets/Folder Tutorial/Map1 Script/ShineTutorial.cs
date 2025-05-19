using UnityEngine;
using BNG;

public class ShineTutorial : MonoBehaviour
{
    public static ShineTutorial Instance;

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
        // Count all puddles at start
        totalDirt = GameObject.FindGameObjectsWithTag("DirtyFloor").Length;
        Debug.Log($"[ShineTutorial] Total dirt: {totalDirt}");

        if (returnZoneTrigger == null)
            Debug.LogError("ReturnZoneTrigger belum di-assign!");

        // Matiin hanya trigger-nya
        var col = returnZoneTrigger.GetComponent<Collider>();
        if (col) col.enabled = false;

        var rz = returnZoneTrigger.GetComponent<ReturnMopZone>();
        if (rz) rz.enabled = false;
    }

    /// <summary>
    /// Call this each time a puddle is cleaned.
    /// </summary>
    public void DirtCleaned()
    {
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