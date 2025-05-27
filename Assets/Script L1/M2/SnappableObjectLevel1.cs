using BNG;
using UnityEngine;

public class SnappableObjectLevel1 : MonoBehaviour
{
    private SnapPointLevel1 currentSnapPoint;
    private Grabbable grabbable;
    private bool isSnapped = false;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Transform originalParent;
    private float snapThreshold = 0.001f;
    private Rigidbody rb;
    private bool wasKinematic;
    private CollisionDetectionMode originalCollisionMode;
    private bool wasBeingHeld = false;
    public SnapPointLevel1 initialSnapPoint;
    private float snapCooldown = 0.2f;
    private float lastSnapTime = 0f;

    void Start()
    {
        grabbable = GetComponent<Grabbable>();
        rb = GetComponent<Rigidbody>();

        if (grabbable == null)
        {
            Debug.LogError("SnappableObjectLevel1 requires a Grabbable component!");
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

        if (initialSnapPoint != null && !initialSnapPoint.isOccupied)
        {
            SnapToPoint(initialSnapPoint);
        }
    }

    void Update()
    {
        // Kondisi saat dilepas
        if (wasBeingHeld && !grabbable.BeingHeld)
        {
            CheckForSnapPoints();
        }

        // Cek apakah keluar dari snap point
        if (grabbable.BeingHeld && isSnapped && currentSnapPoint != null)
        {
            float distance = Vector3.Distance(transform.position, currentSnapPoint.transform.position);
            if (distance > currentSnapPoint.snapRadius)
            {
                ReleaseFromSnap();
            }
        }

        wasBeingHeld = grabbable.BeingHeld;

        // Smooth correction saat sudah tersnap
        if (!grabbable.BeingHeld && isSnapped && currentSnapPoint != null)
        {
            float distanceToSnapPoint = Vector3.Distance(transform.position, currentSnapPoint.transform.position);
            if (distanceToSnapPoint > snapThreshold)
            {
                transform.position = Vector3.Lerp(transform.position, currentSnapPoint.transform.position, Time.deltaTime * currentSnapPoint.snapSpeed);
                transform.rotation = Quaternion.Lerp(transform.rotation, currentSnapPoint.transform.rotation, Time.deltaTime * currentSnapPoint.snapSpeed);
            }
            else
            {
                transform.position = currentSnapPoint.transform.position;
                transform.rotation = currentSnapPoint.transform.rotation;
            }
        }
    }

    void CheckForSnapPoints()
    {
        if (Time.time < lastSnapTime + snapCooldown) return;

        SnapPointLevel1[] snapPoints = FindObjectsOfType<SnapPointLevel1>();
        SnapPointLevel1 closestSnapPoint = null;
        float closestDistance = float.MaxValue;

        foreach (SnapPointLevel1 snapPoint in snapPoints)
        {
            if (!snapPoint.isOccupied)
            {
                float distance = Vector3.Distance(transform.position, snapPoint.transform.position);
                if (distance < closestDistance && snapPoint.IsWithinSnapRadius(transform.position))
                {
                    closestDistance = distance;
                    closestSnapPoint = snapPoint;
                }
            }
        }

        if (closestSnapPoint != null)
        {
            SnapToPoint(closestSnapPoint);
        }
    }

    void SnapToPoint(SnapPointLevel1 snapPoint)
    {
        currentSnapPoint = snapPoint;
        isSnapped = true;
        lastSnapTime = Time.time;

        transform.position = snapPoint.transform.position;
        transform.rotation = snapPoint.transform.rotation;
        transform.SetParent(snapPoint.transform);

        if (rb != null)
        {
            rb.isKinematic = true;
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        }

        // Call snap function in snap point
        snapPoint.SnapObject(transform);
    }

    void ReleaseFromSnap()
    {
        if (currentSnapPoint != null)
        {
            currentSnapPoint.ReleaseObject();
            transform.SetParent(originalParent);
            currentSnapPoint = null;
            isSnapped = false;

            if (rb != null)
            {
                rb.isKinematic = wasKinematic;
                rb.collisionDetectionMode = originalCollisionMode;
            }

            lastSnapTime = Time.time;
        }
    }

    public void ResetPosition()
    {
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        transform.SetParent(originalParent);
        ReleaseFromSnap();
    }
}
