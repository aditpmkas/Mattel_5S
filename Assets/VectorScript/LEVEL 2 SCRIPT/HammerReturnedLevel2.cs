using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerReturnedLevel2 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hammer"))
        {
            ProgressManagerLevel2.Instance.hammerReturned = true;
            Debug.Log("Hammer is returned");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hammer"))
        {
            ProgressManagerLevel2.Instance.hammerReturned = false;
            Debug.Log("Hammer is not returned");
        }
    }
}
