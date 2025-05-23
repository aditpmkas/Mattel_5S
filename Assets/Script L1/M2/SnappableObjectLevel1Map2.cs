using System;
using UnityEngine;
using BNG;

public class SnappableObjectl1m2 : MonoBehaviour
{
    public event Action OnSnapped;
    public event Action OnReleased;

    private SnapPointLevel1Map2 currentSnapPoint;
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
    public SnapPointLevel1Map2 initialSnapPoint;

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
        SnapPointLevel1Map2[] snapPoints = FindObjectsOfType<SnapPointLevel1Map2>();
        SnapPointLevel1Map2 closestSnapPoint = null;
        float closestDistance = float.MaxValue;

        foreach (SnapPointLevel1Map2 snapPoint in snapPoints)
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
