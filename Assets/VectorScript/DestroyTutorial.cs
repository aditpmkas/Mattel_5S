using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTurorial : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!GamePhaseManager.Instance.IsPhase(GamePhaseManager.Phase.Sorting)) return;

        if (other.CompareTag("Sort"))
        {
            Destroy(other.gameObject);
            FindObjectOfType<SortingChecker>()?.IncrementCorrectSort();
        }
        else if (other.CompareTag("Unsort"))
        {
            Destroy(other.gameObject);
            // Bisa tambahkan penalti atau feedback di sini jika perlu
        }
    }
}
