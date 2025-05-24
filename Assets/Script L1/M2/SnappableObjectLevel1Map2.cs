using UnityEngine;
using BNG;

public class SnappableObjectl1m2 : MonoBehaviour
{
    public SnapPointLevel1Map2 currentSnapPoint;
    private Grabbable grabbable;
    private Rigidbody rb;

    private bool isSnapping = false;
    private bool isSnapped = false;

    public float snapSpeed = 10f;

    void Start()
    {
        grabbable = GetComponent<Grabbable>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (grabbable.BeingHeld)
        {
            isSnapping = false; // Jika dipegang, cancel proses snapping
            if (isSnapped)
            {
                // Jika sebelumnya sudah snap, release snap point
                currentSnapPoint.ReleaseObject();
                isSnapped = false;
                currentSnapPoint = null;
                rb.isKinematic = false;
                transform.SetParent(null);
            }
        }
        else
        {
            if (!isSnapped)
            {
                // Cari snap point terdekat
                SnapPointLevel1Map2 nearestSnap = FindNearestSnapPoint();
                if (nearestSnap != null && nearestSnap.IsWithinSnapRadius(transform.position) && !nearestSnap.isOccupied)
                {
                    currentSnapPoint = nearestSnap;
                    isSnapping = true;
                    rb.isKinematic = true;
                }
                else
                {
                    isSnapping = false;
                    rb.isKinematic = false;
                }
            }

            if (isSnapping && currentSnapPoint != null)
            {
                // Gerakkan objek ke snap point dengan smooth
                transform.position = Vector3.MoveTowards(transform.position, currentSnapPoint.transform.position, snapSpeed * Time.deltaTime);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, currentSnapPoint.transform.rotation, snapSpeed * 100f * Time.deltaTime);

                // Jika sudah sampai posisi snap
                if (Vector3.Distance(transform.position, currentSnapPoint.transform.position) < 0.01f &&
                    Quaternion.Angle(transform.rotation, currentSnapPoint.transform.rotation) < 1f)
                {
                    isSnapped = true;
                    isSnapping = false;
                    transform.position = currentSnapPoint.transform.position;
                    transform.rotation = currentSnapPoint.transform.rotation;
                    transform.SetParent(currentSnapPoint.transform);
                    currentSnapPoint.SnapObject(transform);
                }
            }
        }
    }

    SnapPointLevel1Map2 FindNearestSnapPoint()
    {
        SnapPointLevel1Map2[] snapPoints = FindObjectsOfType<SnapPointLevel1Map2>();
        SnapPointLevel1Map2 nearest = null;
        float minDist = Mathf.Infinity;

        foreach (var sp in snapPoints)
        {
            float dist = Vector3.Distance(transform.position, sp.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = sp;
            }
        }
        return nearest;
    }

    public bool IsFullySnapped()
    {
        return isSnapped;
    }
}
