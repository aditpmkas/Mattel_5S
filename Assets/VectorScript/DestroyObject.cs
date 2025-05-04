using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if(gameObject.tag == "TrashCan")
        {
            Destroy(other.gameObject);
            Debug.Log("GameObject " + other.gameObject.name + " destroyed.");
        }
        // Menghancurkan gameObject ini saat menyentuh trigger collider
        
    }
}


