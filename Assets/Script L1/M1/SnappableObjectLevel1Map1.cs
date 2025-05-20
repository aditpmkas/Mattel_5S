using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;

public class SnappableObjectLevel1Map1 : MonoBehaviour
{
    private SnapPoint currentSnapPoint;
    private Grabbable grabbable;
    private bool isSnapped = false;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Transform originalParent;
    private float snapThreshold = 0.001f; // Threshold untuk menghentikan pergerakan
    private Rigidbody rb;
    private bool wasKinematic;
    private CollisionDetectionMode originalCollisionMode;
    private bool isInSnapRange = false;
    private bool wasBeingHeld = false;
    public SnapPoint initialSnapPoint;


    void Start()
    {
        grabbable = GetComponent<Grabbable>();
        rb = GetComponent<Rigidbody>();

        if (grabbable == null)
        {
            Debug.LogError("SnappableObject requires a Grabbable component!");
            enabled = false;
            return;
        }

        // Store original transform data
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
            // tandai state internal
            currentSnapPoint = initialSnapPoint;
            isSnapped = true;

            // reservasi di snap point
            initialSnapPoint.SnapObject(transform);

            // langsung set transform & parent
            transform.position = initialSnapPoint.transform.position;
            transform.rotation = initialSnapPoint.transform.rotation;
            transform.SetParent(initialSnapPoint.transform);

            // jika punya rigidbody, kinematic agar tidak jatuh
            if (rb != null)
            {
                rb.isKinematic = true;
                rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            }
        }
    }

    void Update()
    {
        // Check if object was just released
        if (wasBeingHeld && !grabbable.BeingHeld && isSnapped)
        {
            // Object was just released while snapped
            if (rb != null)
            {
                rb.isKinematic = true;
                rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
                transform.SetParent(currentSnapPoint.transform);
            }
        }
        wasBeingHeld = grabbable.BeingHeld;

        if (grabbable.BeingHeld)
        {
            // Check for nearby snap points when the object is being held
            CheckForSnapPoints();
        }
        else if (isSnapped && currentSnapPoint != null)
        {
            // Cek jarak ke snap point
            float distanceToSnapPoint = Vector3.Distance(transform.position, currentSnapPoint.transform.position);

            if (distanceToSnapPoint > snapThreshold)
            {
                // Smoothly move to snap point position
                transform.position = Vector3.Lerp(transform.position, currentSnapPoint.transform.position, Time.deltaTime * currentSnapPoint.snapSpeed);
                transform.rotation = Quaternion.Lerp(transform.rotation, currentSnapPoint.transform.rotation, Time.deltaTime * currentSnapPoint.snapSpeed);
            }
            else
            {
                // Langsung set ke posisi dan rotasi yang tepat
                transform.position = currentSnapPoint.transform.position;
                transform.rotation = currentSnapPoint.transform.rotation;
            }
        }
    }

    void CheckForSnapPoints()
    {
        // Find all snap points in the scene
        SnapPoint[] snapPoints = FindObjectsOfType<SnapPoint>();
        SnapPoint closestSnapPoint = null;
        float closestDistance = float.MaxValue;

        foreach (SnapPoint snapPoint in snapPoints)
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

        // If we found a valid snap point and we're not already snapped
        if (closestSnapPoint != null && !isSnapped)
        {
            currentSnapPoint = closestSnapPoint;
            isSnapped = true;
            currentSnapPoint.SnapObject(transform);
            isInSnapRange = true;
        }
        else if (closestSnapPoint == null && isSnapped)
        {
            // If we're no longer near any snap point, release
            ReleaseFromSnap();
        }
    }

    void ReleaseFromSnap()
    {
        if (currentSnapPoint != null)
        {
            // Before releasing, inform the bookshelf
            NotifyShelfExit();

            currentSnapPoint.ReleaseObject();
            transform.SetParent(originalParent);
            currentSnapPoint = null;
            isSnapped = false;
            isInSnapRange = false;

            if (!grabbable.BeingHeld)
            {
                // Restore physics only if not held
                if (rb != null)
                {
                    rb.isKinematic = wasKinematic;
                    rb.collisionDetectionMode = originalCollisionMode;
                }
            }
        }
    }

    private void NotifyShelfExit()
    {
        Collider[] overlapping = Physics.OverlapSphere(transform.position, 0.1f);
        foreach (var collider in overlapping)
        {
            var shelf = collider.GetComponent<BookShelf>();
            if (shelf != null)
            {
                shelf.SendMessage("OnTriggerExit", GetComponent<Collider>(), SendMessageOptions.DontRequireReceiver);
                break;
            }
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
