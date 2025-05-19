using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTurorialM2 : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sort"))
        {
            Destroy(other.gameObject);
            FindObjectOfType<SortingTutorialM2>()?.IncrementCorrectSort();
        }
        else if (other.CompareTag("Unsort"))
        {
            Destroy(other.gameObject);
            // Tambahkan feedback atau efek penalti jika ingin
        }
    }
}
