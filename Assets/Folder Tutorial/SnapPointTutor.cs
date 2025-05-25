using UnityEngine;

public class SnapPointT : MonoBehaviour
{
    [Header("Snap Settings")]
    public float snapRadius = 0.5f;
    public float snapSpeed = 5f;

    [Header("Reference Objects")]
    public Transform correctObject;
    public Transform snappedObject;
    public bool isOccupied => snappedObject != null;

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip snapSound;

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
            return string.Equals(snappedClean, correctClean, System.StringComparison.OrdinalIgnoreCase);
        }
    }

    public void SnapObject(Transform objectToSnap, bool playSound = true)
    {
        bool isCorrect = objectToSnap.name == correctObject.name;

        snappedObject = objectToSnap;
        objectToSnap.position = transform.position;
        objectToSnap.rotation = transform.rotation;
        objectToSnap.SetParent(transform);


        Debug.Log($"Snapped: {objectToSnap.name} → {gameObject.name} (correct? {isCorrect})");

        if (playSound)
        {
            PlaySound(snapSound); // Mainkan suara saat snap (kecuali initial snap)
        }

        if (!isCorrect)
            Debug.LogWarning("Wrong object snapped.");
    }

    public void ReleaseObject()
    {
        if (snappedObject != null)
        {
            snappedObject.SetParent(null);
            snappedObject = null;
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
