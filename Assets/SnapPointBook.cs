using UnityEngine;

public class SnapPointBook : MonoBehaviour
{
    public float snapRadius = 0.5f; // Radius within which objects will snap
    public float snapSpeed = 10f; // How fast objects snap to this point
    public bool isOccupied = false; // Whether this snap point currently has an object
    public Transform snappedObject; // Reference to the currently snapped object

    private void OnDrawGizmos()
    {
        // Visualize the snap radius in the editor
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
        }
    }

    public void ReleaseObject()
    {
        snappedObject = null;
        isOccupied = false;
    }
}