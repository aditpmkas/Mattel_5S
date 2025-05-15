using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManagerLevel2 : MonoBehaviour
{
    public AudioSource bgmSource;

    void Start()
    {
        if (bgmSource != null)
        {
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }
}
