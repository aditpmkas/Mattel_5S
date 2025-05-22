using UnityEngine;

public class SnapPointTutorial : MonoBehaviour
{
    [Header("Snap Settings")]
    public float snapRadius = 0.5f;
    public float snapSpeed = 5f;

    [Header("Reference Objects")]
    public Transform correctObject;   // Objek yang seharusnya masuk ke snap point ini
    public Transform snappedObject;
    public bool isOccupied => snappedObject != null;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, snapRadius);
    }

    public bool IsWithinSnapRadius(Vector3 objectPosition)
    {
        return Vector3.Distance(transform.position, objectPosition) <= snapRadius;
    }

    public bool IsCorrectlySnapped
    {
        get
        {
            if (snappedObject == null) return false;
            string snappedClean = snappedObject.name.Replace("(Clone)", "").Trim();
            string correctClean = correctObject.name.Trim();
            return string.Equals(snappedClean, correctClean,
                                 System.StringComparison.OrdinalIgnoreCase);
        }
    }

    public void SnapObject(Transform objectToSnap)
    {
        bool isCorrect = objectToSnap.name == correctObject.name;

        snappedObject = objectToSnap;
        objectToSnap.position = transform.position;
        objectToSnap.rotation = transform.rotation;
        objectToSnap.SetParent(transform);

        Debug.Log($"Snapped: {objectToSnap.name} → {gameObject.name} (correct? {isCorrect})");

        // panggil pengecekan semua snap point
        SetInOrderM2.Instance.CheckAllSnapPointsTutorial();

        if (!isCorrect)
            Debug.LogWarning("Wrong object snapped.");
    }


    public void ReleaseObject()
    {
        if (snappedObject != null)
        {
            snappedObject.SetParent(null);
            snappedObject = null;

            // setelah dilepas, cek ulang semua snap point
            SetInOrderM2.Instance.CheckAllSnapPointsTutorial();
        }
    }
}
