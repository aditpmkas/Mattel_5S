using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{

    int score = 0;  

    private void OnTriggerEnter(Collider other)
    {


            if (other.gameObject.tag == "Sort")
            {
                score++;
                Destroy(other.gameObject);
                Debug.Log("GameObject " + other.gameObject.name + " destroyed.");
                Debug.Log("Your score is: " + score);
            }
            if(other.gameObject.tag == "Unsort")
            {
                score--;
                Destroy(other.gameObject);
                Debug.Log("GameObject " + other.gameObject.name + " destroyed.");
                Debug.Log("Your score is: " + score);
            }
        
        // Menghancurkan gameObject ini saat menyentuh trigger collider
        
    }
}


