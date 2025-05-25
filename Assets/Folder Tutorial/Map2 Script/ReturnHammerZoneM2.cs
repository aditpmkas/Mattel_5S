using UnityEngine;
using BNG;
using System.Collections;

public class ReturnHammerZoneM2 : MonoBehaviour
{
    public string returnMessage = "Hammer returned!";
    public float returnDelay = 2f;
    private bool hasReturned = false;

    private void OnTriggerEnter(Collider other)
    {
        // **Only** the HammerTool should trigger this zone
        if (!hasReturned
           && other.CompareTag("Hammer")
           && other.GetComponent<SnapTutorial>() != null)
        {
            StartCoroutine(HandleReturn(other));
        }
    }

    private IEnumerator HandleReturn(Collider other)
    {
        yield return new WaitForSeconds(returnDelay);

        // Double-check it’s still the hammer
        if (other.CompareTag("Hammer"))
        {
            ShineTutorialM2.Instance.HammerReturned();
            hasReturned = true;
            Debug.Log(returnMessage);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Only reset when the hammer actually leaves
        if (hasReturned && other.CompareTag("Hammer"))
        {
            ShineTutorialM2.Instance.ResetHammerReturned();
            hasReturned = false;
            Debug.Log("Hammer left zone → reset return flag.");
        }
    }
}
