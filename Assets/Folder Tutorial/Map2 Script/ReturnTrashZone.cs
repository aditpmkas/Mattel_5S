using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnTrashZoneM2 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the thing entering is a TrashItem
        if (other.CompareTag("TrashItem"))
        {
            // Notify the tutorial manager
            ShineTutorialM2.Instance.TrashItemReturned();

            // Optionally, destroy or deactivate the trash object
            Destroy(other.gameObject);
        }
    }
}