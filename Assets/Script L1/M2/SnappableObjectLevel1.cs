using UnityEngine;
using BNG;

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
    private float snapCooldown = 0.2f; // Cooldown to prevent rapid snap/release
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
            currentSnapPoint = initialSnapPoint;
            isSnapped = true;
            initialSnapPoint.SnapObject(transform);
            transform.position = initialSnapPoint.transform.position;
            transform.rotation = initialSnapPoint.transform.rotation;
            transform.SetParent(initialSnapPoint.transform);

            if (rb != null)
            {
                rb.isKinematic = true;
                rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            }
        }
    }

    void Update()
    {
        if (wasBeingHeld && !grabbable.BeingHeld && isSnapped && currentSnapPoint != null)
        {
            // When released, lock to snap point
            rb.isKinematic = true;
            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            transform.SetParent(currentSnapPoint.transform);
            transform.position = currentSnapPoint.transform.position;
            transform.rotation = currentSnapPoint.transform.rotation;
        }

        wasBeingHeld = grabbable.BeingHeld;

        if (grabbable.BeingHeld)
        {
            // Only check for new snap points if not snapped or moved far enough
            if (isSnapped && currentSnapPoint != null)
            {
                float distanceToSnapPoint = Vector3.Distance(transform.position, currentSnapPoint.transform.position);
                if (distanceToSnapPoint > currentSnapPoint.snapRadius)
                {
                    ReleaseFromSnap();
                }
            }
            else
            {
                CheckForSnapPoints();
            }
        }
        else if (isSnapped && currentSnapPoint != null)
        {
            // Smoothly move to snap point if slightly off
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
        // Skip if on cooldown
        if (Time.time < lastSnapTime + snapCooldown)
            return;

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

        if (closestSnapPoint != null && !isSnapped)
        {
            currentSnapPoint = closestSnapPoint;
            isSnapped = true;
            currentSnapPoint.SnapObject(transform);
            lastSnapTime = Time.time;

            if (currentSnapPoint.correctObject != transform)
            {
                Debug.LogWarning($"Object {gameObject.name} snapped to the wrong point: {currentSnapPoint.gameObject.name}.");
            }
        }
    }

    void ReleaseFromSnap()
    {
        if (currentSnapPoint != null)
        {
            currentSnapPoint.ReleaseObject();
            transform.SetParent(originalParent);
            currentSnapPoint = null;
            isSnapped = false;

            if (rb != null && !grabbable.BeingHeld)
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