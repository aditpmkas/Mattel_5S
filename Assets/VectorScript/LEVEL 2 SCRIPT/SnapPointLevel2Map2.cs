using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapPointLevel2Map2 : MonoBehaviour
{
    public float snapRadius = 0.5f;
    public float snapSpeed = 10f;
    public bool isOccupied = false;
    public Transform snappedObject;

    public AudioSource snapSFX; // Optional snap sound

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

            // No sound here anymore — wait for final snap moment

            // Notify scoring if this point has a ToolSortingToolLevel2Map2 script
            var sortingScript = GetComponent<ToolSortingToolLevel2Map2>();
            if (sortingScript != null)
            {
                sortingScript.OnSnap(objectToSnap.gameObject);
            }
        }
    }

    public void ReleaseObject()
    {
        if (snappedObject != null)
        {
            var sortingScript = GetComponent<ToolSortingToolLevel2Map2>();
            if (sortingScript != null)
            {
                sortingScript.OnRelease();
            }
        }

        snappedObject = null;
        isOccupied = false;
    }
}
