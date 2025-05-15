using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

public class GameStartManagerLevel2 : MonoBehaviour
{
    private Grabbable[] allGrabbables;

    void Start()
    {
        // Find and disable all grabbables at the start
        allGrabbables = FindObjectsOfType<Grabbable>();

        foreach (Grabbable grabbable in allGrabbables)
        {
            grabbable.enabled = false;
        }

        Debug.Log("Grabbables disabled at start.");
    }

    // Call this when the Start button is pressed
    public void EnableGrabbables()
    {
        foreach (Grabbable grabbable in allGrabbables)
        {
            grabbable.enabled = true;
        }

        Debug.Log("Grabbables enabled, game started!");
    }
}
