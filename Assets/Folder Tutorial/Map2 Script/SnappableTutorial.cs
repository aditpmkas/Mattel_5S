using UnityEngine;
using BNG;

public class SnappableObjectTutorial : MonoBehaviour
{
    private SnapPointTutorial currentSnapPoint;
    private Grabbable grabbable;
    private Rigidbody rb;
    private bool wasBeingHeld = false;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Transform originalParent;
    private bool wasKinematic;
    private CollisionDetectionMode originalCollisionMode;

    void Start()
    {
        grabbable = GetComponent<Grabbable>();
        rb = GetComponent<Rigidbody>();

        if (grabbable == null)
        {
            Debug.LogError("SnappableObjectTutorial requires Grabbable component!");
            enabled = false;
            return;
        }

        originalPosition = transform.position;
        originalRotation = transform.rotation;
        originalParent = transform.parent;

        if (rb != null)
        {
            wasKinematic = rb.isKinematic;
            originalCollisionMode = rb.collisionDetectionMode;
        }
    }

    void Update()
    {
        // Ketika baru dilepas
        if (wasBeingHeld && !grabbable.BeingHeld)
        {
            CheckForSnapPoints();
        }

        // Smooth Lerp saat tersnap
        if (currentSnapPoint != null && !grabbable.BeingHeld)
        {
            transform.position = Vector3.Lerp(transform.position, currentSnapPoint.transform.position, Time.deltaTime * currentSnapPoint.snapSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, currentSnapPoint.transform.rotation, Time.deltaTime * currentSnapPoint.snapSpeed);
        }

        wasBeingHeld = grabbable.BeingHeld;
    }

    void CheckForSnapPoints()
    {
        SnapPointTutorial[] allPoints = FindObjectsOfType<SnapPointTutorial>();
        SnapPointTutorial closest = null;
        float closestDist = float.MaxValue;

        foreach (var point in allPoints)
        {
            if (!point.isOccupied || point.snappedObject == transform)
            {
                float dist = Vector3.Distance(transform.position, point.transform.position);
                if (dist < closestDist && point.IsWithinSnapRadius(transform.position))
                {
                    closest = point;
                    closestDist = dist;
                }
            }
        }

        if (closest != null)
        {
            if (currentSnapPoint != null && currentSnapPoint != closest)
            {
                currentSnapPoint.ReleaseObject();
            }

            currentSnapPoint = closest;
            currentSnapPoint.SnapObject(transform);

            if (rb != null)
            {
                rb.isKinematic = true;
                rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            }
        }
        else
        {
            // Jauh dari snap point, lepaskan dari snap
            if (currentSnapPoint != null)
            {
                currentSnapPoint.ReleaseObject();
                currentSnapPoint = null;

                if (rb != null)
                {
                    rb.isKinematic = wasKinematic;
                    rb.collisionDetectionMode = originalCollisionMode;
                }

                transform.SetParent(originalParent);
            }
        }
    }

    public void ResetPosition()
    {
        if (currentSnapPoint != null)
        {
            currentSnapPoint.ReleaseObject();
            currentSnapPoint = null;
        }

        transform.position = originalPosition;
        transform.rotation = originalRotation;
        transform.SetParent(originalParent);

        if (rb != null)
        {
            rb.isKinematic = wasKinematic;
            rb.collisionDetectionMode = originalCollisionMode;
        }
    }
}
