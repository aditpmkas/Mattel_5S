using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapPointLevel2 : MonoBehaviour
{
    public float snapRadius = 0.5f;
    public float snapSpeed = 10f;
    public bool isOccupied = false;
    public Transform snappedObject;

    public AudioSource snapSFX; //  Add this for snap sound

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, snapRadius);
    }

    public bool IsWithinSnapRadius(Vector3 objectPosition)
    {
        return Vector3.Distance(transform.position, objectPosition) <= snapRadius;
    }

    public void SnapObject(Transform objectToSnap)
    {
        if (!isOccupied)
        {
            snappedObject = objectToSnap;
            isOccupied = true;

            //  Play snap SFX if assigned
            if (snapSFX != null)
            {
                snapSFX.Play();
            }
        }
    }

    public void ReleaseObject()
    {
        snappedObject = null;
        isOccupied = false;
    }
}
