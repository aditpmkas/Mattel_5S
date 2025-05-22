using UnityEngine;
using BNG;
using System.Collections;

/// <summary>
/// When the hammer is held in the return zone for a short delay,
/// this completes the HammerReturned event in ShineTutorial.
/// </summary>
public class ReturnHammerZone : MonoBehaviour
{
  
    public string returnMessage = "Hammer returned!";
    public float returnDelay = 2f;

    // Make sure this GameObject has a Collider set to "Is Trigger"
    private void OnTriggerEnter(Collider other)
    {
        // Look for your SnappableObject on the mop
        var snappable = other.GetComponent<SnappableObject>();
        if (snappable != null)
        {
            Debug.Log($"[ReturnMopZone] {returnMessage}");

            StartCoroutine(DelayedReturn());
        }
    }

    private IEnumerator DelayedReturn()
    {
        yield return new WaitForSeconds(returnDelay);

        Debug.Log($"[ReturnHammerZone] {returnMessage}");

        // Notify ShineTutorial
        ShineTutorial.Instance.HammerReturned();

        // Destroy this trigger so it only fires once
        Destroy(gameObject);
    }
}
