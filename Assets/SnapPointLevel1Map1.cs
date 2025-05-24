using UnityEngine;

public class SnapPointLevel1Map1 : MonoBehaviour
{
    public float snapRadius = 0.5f; // Radius dalam unit world untuk snap
    public float snapSpeed = 10f; // Kecepatan snap ke posisi
    public bool isOccupied = false; // Apakah ada objek di snap point ini
    public Transform snappedObject; // Objek yang sedang disnap di sini

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, snapRadius);
    }

    public bool IsWithinSnapRadius(Vector3 objectPosition)
    {
        return Vector3.Distance(transform.position, objectPosition) <= snapRadius;
    }

    public void SnapObject(Transform objectToSnap)
    {
        if (!isOccupied)
        {
            snappedObject = objectToSnap;
            isOccupied = true;
        }
    }

    public void ReleaseObject()
    {
        snappedObject = null;
        isOccupied = false;
    }
}
