using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{

    public int sortScore = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sort")
        {
            sortScore += 100;
            Destroy(other.gameObject);
            Debug.Log("GameObject " + other.gameObject.name + " destroyed.");
            Debug.Log("Your score is: " + sortScore);
        }

        if (other.gameObject.tag == "Unsort")
        {
            sortScore -= 50;
            Destroy(other.gameObject);
            Debug.Log("GameObject " + other.gameObject.name + " destroyed.");
            Debug.Log("Your score is: " + sortScore);
        }
    }

}


