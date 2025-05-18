using UnityEngine;

public class DestroySorterL1M2 : MonoBehaviour
{
    public string targetTag = "ItemToDestroy";

    private void OnTriggerEnter(Collider other)
    {
        // Cek fase aktif
        if (GameManagerL1M2.Instance.currentPhase != GameManagerL1M2.GamePhase.Sorting)
            return;

        if (other.CompareTag(targetTag))
        {
            GameManagerL1M2.Instance.IncrementSortDestroyed();
            Destroy(other.gameObject);
        }
    }
}
