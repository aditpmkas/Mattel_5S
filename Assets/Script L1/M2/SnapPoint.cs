using UnityEngine;

/// <summary>
/// Simple snap-point mechanic: detects nearby objects, snaps them into place, and releases.
/// </summary>
public class SnapPoint : MonoBehaviour
{
    [Tooltip("Radius within which objects will snap")]
    public float snapRadius = 0.5f;
    [Tooltip("Speed at which the object moves into the snap point")]
    public float snapSpeed = 10f;

    private Transform snappedObject;
    private bool isOccupied => snappedObject != null;
    private bool isSnapping = false;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, snapRadius);
    }

    void Update()
    {
        // Smoothly move the object into the snap point
        if (isSnapping && snappedObject != null)
        {
            snappedObject.position = Vector3.MoveTowards(
                snappedObject.position,
                transform.position,
                snapSpeed * Time.deltaTime
            );

            // Check if it has reached the snap position
            if (Vector3.Distance(snappedObject.position, transform.position) < 0.01f)
            {
                snappedObject.position = transform.position;
                isSnapping = false;
            }
        }
    }

    /// <summary>
    /// Determines if a world-space position is within snap radius.
    /// </summary>
    public bool IsWithinSnapRadius(Vector3 objectPosition)
    {
        return Vector3.Distance(transform.position, objectPosition) <= snapRadius;
    }

    /// <summary>
    /// Initiates snapping of the specified object if the point is free.
    /// </summary>
    public void SnapObject(Transform objectToSnap)
    {
        if (isOccupied)
        {
            Debug.LogWarning("SnapPoint is occupied.");
            return;
        }

        snappedObject = objectToSnap;
        snappedObject.SetParent(transform);
        isSnapping = true;
    }

    /// <summary>
    /// Releases the currently snapped object, if any.
    /// </summary>
    public void ReleaseObject()
    {
        if (!isOccupied) return;

        snappedObject.SetParent(null);
        snappedObject = null;
        isSnapping = false;
    }
}
