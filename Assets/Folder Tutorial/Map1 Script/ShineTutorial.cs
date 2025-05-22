using UnityEngine;
using BNG;

public class ShineTutorial : MonoBehaviour
{
    public static ShineTutorial Instance;

    private bool crackIsFixed = false;
    private bool hammerReturned = false;

    [Header("Return Zone Settings")]
    public GameObject returnZone;
    public GameObject returnZoneTrigger;
    public GameObject hammerReturnTrigger;

    private int totalCracks;
    private int cracksFixed = 0;

    private int totalDirt;
    private int cleanedDirt = 0;

    private bool returnZoneEnabled = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        totalCracks = GameObject.FindGameObjectsWithTag("Crack").Length;
        totalDirt = GameObject.FindGameObjectsWithTag("DirtyFloor").Length;

        foreach (var puddle in GameObject.FindGameObjectsWithTag("DirtyFloor"))
        {
            var col = puddle.GetComponent<Collider>();
            if (col != null) col.enabled = false;
        }

        if (hammerReturnTrigger != null)
        {
            var col = hammerReturnTrigger.GetComponent<Collider>();
            if (col) col.enabled = false;
            var rz = hammerReturnTrigger.GetComponent<ReturnHammerZone>();
            if (rz) rz.enabled = false;
        }

        // Disable return zone trigger
        if (returnZoneTrigger == null)
        {
            Debug.LogError("[ShineTutorial] returnZoneTrigger belum di-assign!");
        }   
        else
        {
            var col = returnZoneTrigger.GetComponent<Collider>();
            if (col) col.enabled = false;
            var rz = returnZoneTrigger.GetComponent<ReturnMopZone>();
            if (rz) rz.enabled = false;
        }

        Debug.Log($"[ShineTutorial] TotalCracks={totalCracks}, TotalDirt={totalDirt}");
    }

    public void HammerReturned()
    {
        hammerReturned = true;
        Debug.Log("[ShineTutorial] Hammer has been returned.");
        TryUnlockPuddles();
    }

    private void TryUnlockPuddles()
    {
        if (crackIsFixed && hammerReturned)
        {
            Debug.Log("[ShineTutorial] Conditions met → puddles unlocked.");
            foreach (var puddle in GameObject.FindGameObjectsWithTag("DirtyFloor"))
                puddle.GetComponent<Collider>().enabled = true;
        }
    }

    public void CrackFixed()
    {
        cracksFixed++;
        Debug.Log($"[ShineTutorial] Crack fixed: {cracksFixed}/{totalCracks}");

        if (cracksFixed >= totalCracks)
        {
            crackIsFixed = true;
            Debug.Log("[ShineTutorial] All cracks fixed. Now waiting for hammer return.");

            if (hammerReturnTrigger != null)
            {
                var col = hammerReturnTrigger.GetComponent<Collider>();
                if (col) col.enabled = true;
                var rz = hammerReturnTrigger.GetComponent<ReturnHammerZone>();
                if (rz) rz.enabled = true;
            }

            TryUnlockPuddles();
        }
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