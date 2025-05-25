using UnityEngine;

public class SnapPointLevel1 : MonoBehaviour
{
    [Header("Snap Settings")]
    public float snapRadius = 0.5f;
    public float snapSpeed = 5f;

    [Header("Reference Objects")]
    public Transform correctObject;   // Objek yang harus tersnap di sini
    public Transform snappedObject;   // Objek yang sedang tersnap

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip snapSound;

    public bool isOccupied = false;

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
            isOccupied = true;
            snappedObject = objectToSnap;
            objectToSnap.position = transform.position;
            objectToSnap.rotation = transform.rotation;
            objectToSnap.SetParent(transform);

            Debug.Log($"Trying to snap {objectToSnap.name} to {gameObject.name}. Correct object should be {correctObject?.name}");

            if (objectToSnap == correctObject)
            {
                Debug.Log("Object benar dan berhasil snap.");
                GameManagerL1M2.Instance.CheckAllSnapPoints();

                // Putar suara snap di sini
                if (audioSource != null && snapSound != null)
                {
                    audioSource.PlayOneShot(snapSound);
                }
            }
            else
            {
                Debug.LogWarning("Object snapped to the wrong point.");
            }
        }
    }

    public void ReleaseObject()
    {
        if (snappedObject != null)
        {
            snappedObject.SetParent(null);
        }
        snappedObject = null;
        isOccupied = false;
    }

    private void Update()
    {
        // Cek fase, hanya cek snap saat fase SetInOrder aktif
        if (GameManagerL1M2.Instance.currentPhase == GameManagerL1M2.GamePhase.SetInOrder)
        {
            GameManagerL1M2.Instance.CheckAllSnapPoints();
        }
    }
}
