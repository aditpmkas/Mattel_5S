using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDropSoundLevel2 : MonoBehaviour
{
    public AudioSource waterDropSFX;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WaterDrop"))
        {
            if (waterDropSFX != null)
            {
                waterDropSFX.Play();
                Debug.Log("Water drop triggered the sound!");
            }
        }
    }
}
