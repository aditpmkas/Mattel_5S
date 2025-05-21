using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BNG;

public class HammerButtonActivator : MonoBehaviour
{
    public Grabbable hammer;                      // Assign your hammer Grabbable here
    public Canvas targetCanvas;                    // Assign the canvas you want to enable/disable

    private GraphicRaycaster raycaster;

    void Start()
    {
        if (targetCanvas != null)
        {
            raycaster = targetCanvas.GetComponent<GraphicRaycaster>();
            raycaster.enabled = false; // Disable at start
        }
    }

    void Update()
    {
        if (hammer != null && raycaster != null)
        {
            if (hammer.BeingHeld)
            {
                raycaster.enabled = true;
            }
            else
            {
                raycaster.enabled = false;
            }
        }
    }
}
