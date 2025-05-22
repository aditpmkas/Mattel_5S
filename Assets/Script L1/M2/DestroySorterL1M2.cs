using UnityEngine;

public class DestroySorterL1M2 : MonoBehaviour
{
    [Header("SortBias Target Transforms")]
    public Transform sortBiasATarget;
    public Transform sortBiasBTarget;
    public Transform sortBiasCTarget;

    [Header("ShineChecker Reference")]
    public ShineChecker shineChecker; // Drag di Inspector

    private void OnTriggerEnter(Collider other)
    {
        // Sorting Phase Logic
        if (GameManagerL1M2.Instance.currentPhase == GameManagerL1M2.GamePhase.Sorting)
        {
            string tag = other.tag;

            if (tag == "Sort")
            {
                GameManagerL1M2.Instance.IncrementSortDestroyed();
                Destroy(other.gameObject);
            }
            else if (tag == "UnSort")
            {
                Destroy(other.gameObject);
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

        // Root Cause Logic (boleh aktif di semua fase, atau bisa dibatasi lagi)
        if (other.CompareTag("RootCauseLevel1"))
        {
            if (shineChecker != null)
            {
                shineChecker.RegisterRootCauseHit();
            }

            Destroy(other.gameObject);
        }
    }

    private void MoveToTarget(Transform objectToMove, Transform target)
    {
        objectToMove.SetPositionAndRotation(target.position, target.rotation);
    }
}
