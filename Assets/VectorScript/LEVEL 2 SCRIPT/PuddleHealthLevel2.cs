using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuddleHealthLevel2 : MonoBehaviour
{
    public int maxSwipes = 3;
    private int currentSwipes = 0;

    public bool IsDestroyed()
    {
        return currentSwipes >= maxSwipes;
    }

    public void AddSwipe()
    {
        currentSwipes++;
        Debug.Log("Puddle swiped " + currentSwipes + " times");

        if (currentSwipes >= maxSwipes)
        {
            Destroy(gameObject);
        }
    }
}


