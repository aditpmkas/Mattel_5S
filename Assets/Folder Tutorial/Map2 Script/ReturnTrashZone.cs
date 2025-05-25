using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnTrashZoneM2 : MonoBehaviour
{
    public AudioSource audioTrash;
    public AudioClip clipTrash;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the thing entering is a TrashItem
        if (other.CompareTag("TrashItem"))
        {
            PlayTrashSound();
            ShineTutorialM2.Instance.TrashItemReturned();

            Destroy(other.gameObject);
        }
    }

    private void PlayTrashSound() 
    {
        if (audioTrash != null && clipTrash != null)
        {
            audioTrash.PlayOneShot(clipTrash);
        }
    }
}