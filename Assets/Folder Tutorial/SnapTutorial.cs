using UnityEngine;
using BNG;

public class SnapTutorial : MonoBehaviour
{
    public SnapPointT initialSnapPoint;

    private SnapPointT currentSnapPoint;
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
            Debug.LogError("SnapTutorial requires Grabbable component!");
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

        if (initialSnapPoint != null && initialSnapPoint.snappedObject == null)
        {
            currentSnapPoint = initialSnapPoint;
            currentSnapPoint.SnapObject(transform, false);

            transform.position = currentSnapPoint.transform.position;
            transform.rotation = currentSnapPoint.transform.rotation;
            transform.SetParent(currentSnapPoint.transform);

            if (rb != null)
            {
                rb.isKinematic = true;
                rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            }
        }
    }

    void Update()
    {
        if (wasBeingHeld && !grabbable.BeingHeld)
        {
            CheckForSnapPoints();
        }

        if (currentSnapPoint != null && !grabbable.BeingHeld)
        {
            transform.position = Vector3.Lerp(transform.position, currentSnapPoint.transform.position, Time.deltaTime * currentSnapPoint.snapSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, currentSnapPoint.transform.rotation, Time.deltaTime * currentSnapPoint.snapSpeed);
        }

        wasBeingHeld = grabbable.BeingHeld;
    }

    void CheckForSnapPoints()
    {
        SnapPointT[] allPoints = FindObjectsOfType<SnapPointT>();
        SnapPointT closest = null;
        float closestDist = float.MaxValue;

        foreach (var point in allPoints)
        {
            if (point.snappedObject == null || point.snappedObject == transform)
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
