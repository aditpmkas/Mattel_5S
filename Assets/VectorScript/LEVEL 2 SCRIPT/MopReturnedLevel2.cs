using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MopReturnedLevel2 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mop"))
        {
            ProgressManagerLevel2.Instance.mopReturned = true;
            Debug.Log("Mop is returned");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Mop"))
        {
            ProgressManagerLevel2.Instance.mopReturned = false;
            Debug.Log("Mop is not returned");
        }
    }
}
