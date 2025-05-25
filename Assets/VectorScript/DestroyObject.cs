using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    int score = 0;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip hitSound;

    private void OnTriggerEnter(Collider other)
    {
        if (!GamePhaseManager.Instance.IsPhase(GamePhaseManager.Phase.Sorting)) return;

        string tag = other.gameObject.tag;

        // List tag yang ingin kita play sound saat interact
        string[] tagsWithSound = { "Sort", "Unsort", "SortBiasA", "SortBiasB", "SortBiasC" };

        bool shouldPlaySound = false;
        foreach (string t in tagsWithSound)
        {
            if (tag == t)
            {
                shouldPlaySound = true;
                break;
            }
        }

        if (shouldPlaySound)
        {
            if (audioSource != null && hitSound != null)
            {
                audioSource.PlayOneShot(hitSound);
            }
        }

        if (tag == "Sort")
        {
            score++;
            Destroy(other.gameObject);
            Debug.Log("GameObject " + other.gameObject.name + " destroyed.");
            Debug.Log("Your score is: " + score);

            FindObjectOfType<SortingChecker>().IncrementCorrectSort();
        }
        else if (tag == "Unsort")
        {
            score--;
            Destroy(other.gameObject);
            Debug.Log("GameObject " + other.gameObject.name + " destroyed.");
            Debug.Log("Your score is: " + score);
        }
        else if (tag == "SortBiasA" || tag == "SortBiasB" || tag == "SortBiasC")
        {
            ReturnSortBias(other.gameObject, tag);
        }
    }

    private void ReturnSortBias(GameObject obj, string tag)
    {
        switch (tag)
        {
            case "SortBiasA":
                obj.transform.position = new Vector3(4.314104f, 3.635044f, 2.310474f);
                obj.transform.rotation = Quaternion.identity;
                break;
            case "SortBiasB":
                obj.transform.position = new Vector3(4.07222f, 3.637642f, 2.413f);
                obj.transform.rotation = Quaternion.Euler(0f, -43.482f, 0f);
                break;
            case "SortBiasC":
                obj.transform.position = new Vector3(4.037f, 3.635f, 3.035f);
                obj.transform.rotation = Quaternion.identity;
                break;
        }

        Debug.Log($"[DestroyObject] {tag} dikembalikan ke posisi awal.");
    }
}
