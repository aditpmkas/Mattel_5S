using System;
using UnityEngine;
using BNG;

public class SnappableObjectLevel1Map1 : MonoBehaviour
{
    public event Action OnSnapped;
    public event Action OnReleased;

    private SnapPointLevel1Map1 currentSnapPoint; // Ganti SnapPoint jadi SnapPointLevel1Map1
    private Grabbable grabbable;
    private bool isSnapped = false;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Transform originalParent;
    private Rigidbody rb;
    private bool wasKinematic;
    private CollisionDetectionMode originalCollisionMode;
    private bool wasBeingHeld = false;
    public SnapPointLevel1Map1 initialSnapPoint; // Ganti ke SnapPointLevel1Map1

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

            OnSnapped?.Invoke();
        }
    }

    void Update()
    {
        if (wasBeingHeld && !grabbable.BeingHeld && isSnapped)
        {
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
            CheckForSnapPoints();
        }
        else if (isSnapped && currentSnapPoint != null)
        {
            // Gunakan snapRadius sebagai threshold
            float distanceToSnapPoint = Vector3.Distance(transform.position, currentSnapPoint.transform.position);
            float threshold = currentSnapPoint.snapRadius * 1.1f; // dikasih buffer sedikit

            if (distanceToSnapPoint > threshold)
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
        SnapPointLevel1Map1[] snapPoints = FindObjectsOfType<SnapPointLevel1Map1>();
        SnapPointLevel1Map1 closestSnapPoint = null;
        float closestDistance = float.MaxValue;

        foreach (SnapPointLevel1Map1 snapPoint in snapPoints)
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
            OnSnapped?.Invoke();
        }
        else if (closestSnapPoint == null && isSnapped)
        {
            ReleaseFromSnap();
            OnReleased?.Invoke();
        }
    }

    void ReleaseFromSnap()
    {
        if (currentSnapPoint != null)
        {
            NotifyShelfExit();

            currentSnapPoint.ReleaseObject();
            transform.SetParent(originalParent);
            currentSnapPoint = null;
            isSnapped = false;

            if (!grabbable.BeingHeld)
            {
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
