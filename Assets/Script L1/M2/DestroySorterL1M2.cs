using UnityEngine;
using System.Collections;

public class DestroySorterL1M2 : MonoBehaviour
{
    [Header("SortBias Target Transforms")]
    public Transform sortBiasATarget;
    public Transform sortBiasBTarget;
    public Transform sortBiasCTarget;

    [Header("ShineChecker Reference")]
    public ShineChecker shineChecker; // Drag di Inspector

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip hitSound;

    private void OnTriggerEnter(Collider other)
    {
        string tag = other.tag;

        string[] tagsWithSound = { "SortBias", "Sort", "Unsort", "SortBiasA", "SortBiasB", "SortBiasC", "RootCauseLevel1" };
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
            Debug.Log($"Play sound for tag: {tag}");
            if (audioSource != null && hitSound != null)
            {
                audioSource.PlayOneShot(hitSound);
            }
        }

        if (GameManagerL1M2.Instance.currentPhase == GameManagerL1M2.GamePhase.Sorting)
        {
            if (tag == "Sort")
            {
                GameManagerL1M2.Instance.IncrementSortDestroyed();
                Destroy(other.gameObject);
            }
            else if (tag == "Unsort")
            {
                // Delay destroy biar suara bisa keluar dulu
                StartCoroutine(DestroyWithDelay(other.gameObject, 0.2f));
            }
            else if (tag == "SortBiasA" && sortBiasATarget != null)
            {
                MoveToTarget(other.transform, sortBiasATarget);
            }
            else if (tag == "SortBiasB" && sortBiasBTarget != null)
            {
                MoveToTarget(other.transform, sortBiasBTarget);
            }
            else if (tag == "SortBiasC" && sortBiasCTarget != null)
            {
                MoveToTarget(other.transform, sortBiasCTarget);
            }
        }

        if (other.CompareTag("RootCauseLevel1"))
        {
            if (shineChecker != null)
            {
                shineChecker.RegisterRootCauseHit();
            }

            Destroy(other.gameObject);
        }
    }

    private IEnumerator DestroyWithDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(obj);
    }




    private void MoveToTarget(Transform objectToMove, Transform target)
    {
        objectToMove.SetPositionAndRotation(target.position, target.rotation);
    }
}
