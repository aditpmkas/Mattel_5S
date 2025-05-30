using UnityEngine;
using BNG;
using System.Collections;

/// <summary>
/// When the mop enters the return zone, completes the Shine task
/// and resets the mop back to its original snap point.
/// </summary>
public class ReturnMopZone : MonoBehaviour
{
    [Tooltip("Optional return message")]
    public string returnMessage = "Mop returned!";
    public float completeDelay = 2f;

    // Make sure this GameObject has a Collider set to "Is Trigger"
    private void OnTriggerEnter(Collider other)
    {
        // Look for your SnappableObject on the mop
        var snappable = other.GetComponent<SnapTutorial>();
        if (snappable != null)
        {
            Debug.Log($"[ReturnMopZone] {returnMessage}");

            StartCoroutine(DelayedComplete());
        }
    }

    private IEnumerator DelayedComplete ()
    {
        yield return new WaitForSeconds(completeDelay);

        TaskManager .Instance.CompleteTask(TaskType.Shine);

        Destroy(gameObject);
    }
}
