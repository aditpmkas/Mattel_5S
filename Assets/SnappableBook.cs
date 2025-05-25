using System;
using UnityEngine;
using BNG;

public class SnappableBook : MonoBehaviour
{
    public event Action OnSnapped;
    public event Action OnReleased;

    public SnapPointBook initialSnapPoint;
    public AudioSource snapAudio; // <-- AudioSource untuk suara "buku ditaro"

    private SnapPointBook currentSnapPoint;
    private Grabbable grabbable;
    private Rigidbody rb;

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Transform originalParent;
    private bool wasKinematic;
    private CollisionDetectionMode originalCollisionMode;
    private bool wasBeingHeld = false;

    private bool isSnapped = false;
    private float snapThreshold = 0.001f;

    void Start()
    {
        grabbable = GetComponent<Grabbable>();
        rb = GetComponent<Rigidbody>();

        if (grabbable == null)
        {
            Debug.LogError("SnappableBook requires a Grabbable component!");
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

            currentSnapPoint.SnapObject(transform);
            transform.position = currentSnapPoint.transform.position;
            transform.rotation = currentSnapPoint.transform.rotation;
            transform.SetParent(currentSnapPoint.transform);

            if (rb != null)
            {
                rb.isKinematic = true;
                rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            }

            PlaySnapAudio(); // mainkan suara saat snap awal

            OnSnapped?.Invoke();
        }
    }

    void Update()
    {
        if (grabbable.BeingHeld && rb != null && rb.isKinematic)
        {
            rb.isKinematic = false;
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            transform.SetParent(null);
        }

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
        SnapPointBook[] snapPoints = FindObjectsOfType<SnapPointBook>();
        SnapPointBook closestSnapPoint = null;
        float closestDistance = float.MaxValue;

        foreach (SnapPointBook snapPoint in snapPoints)
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
            transform.position = currentSnapPoint.transform.position;
            transform.rotation = currentSnapPoint.transform.rotation;
            transform.SetParent(currentSnapPoint.transform);

            if (rb != null)
            {
                rb.isKinematic = true;
                rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            }

            PlaySnapAudio(); // mainkan suara saat snap

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
            currentSnapPoint.ReleaseObject();
            transform.SetParent(originalParent);
            currentSnapPoint = null;
            isSnapped = false;

            if (!grabbable.BeingHeld && rb != null)
            {
                rb.isKinematic = wasKinematic;
                rb.collisionDetectionMode = originalCollisionMode;
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

    private void PlaySnapAudio()
    {
        if (snapAudio != null)
        {
            snapAudio.Play();
        }
    }
}
