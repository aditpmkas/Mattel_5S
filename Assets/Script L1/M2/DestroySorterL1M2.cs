using UnityEngine;

public class DestroySorterL1M2 : MonoBehaviour
{
    [Header("SortBias Target Transforms")]
    public Transform sortBiasATarget;
    public Transform sortBiasBTarget;
    public Transform sortBiasCTarget;

    private void OnTriggerEnter(Collider other)
    {
        if (GameManagerL1M2.Instance.currentPhase != GameManagerL1M2.GamePhase.Sorting)
            return;

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

    private void MoveToTarget(Transform objectToMove, Transform target)
    {
        objectToMove.SetPositionAndRotation(target.position, target.rotation);
    }
}
