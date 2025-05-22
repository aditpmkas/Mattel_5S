using UnityEngine;
using BNG;

public class TwoHandedGrab : MonoBehaviour
{
    public Grabbable leftHandGrab;
    public Grabbable rightHandGrab;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    void Update()
    {
        if (leftHandGrab != null && rightHandGrab != null)
        {
            if (leftHandGrab.BeingHeld && rightHandGrab.BeingHeld)
            {
                rb.isKinematic = false;
            }
            else
            {
                rb.isKinematic = true;
            }
        }
    }
}
