using BNG;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CrackFixM2 : MonoBehaviour
{
    [Header("Drip Particle")]
    public ParticleSystem dripPS;

    public Grabbable hammer;
    public Canvas targetCanvas;
    private GraphicRaycaster raycaster;

    private bool isFixed = false;

    private void Start()
    {
        if (targetCanvas != null)
        {
            raycaster = targetCanvas.GetComponent<GraphicRaycaster>();
            raycaster.enabled = false; // Disable at start
        }

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

    public void FixCrack()
    {
        if (isFixed) return;
        isFixed = true;

        if (dripPS != null) dripPS.Stop();

        if (ShineTutorialM2.Instance != null)
            ShineTutorialM2.Instance.CrackFixed();
        else
            Debug.LogWarning("ShineTutorial.Instance null!");

        Debug.Log("Crack fixed! Puddles now unlocked.");

        Destroy(gameObject);
    }
}
