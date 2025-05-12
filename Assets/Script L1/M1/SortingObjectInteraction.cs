using UnityEngine;

public class SortingObjectInteraction : MonoBehaviour
{
    public GameObject sortingObject;
    private bool canInteract = true;

    private void Update()
    {
        // Membatasi interaksi dengan objek sorting berdasarkan fase
        if (GamePhaseManager.Instance.IsPhase(GamePhaseManager.Phase.Sorting) ||
            GamePhaseManager.Instance.IsPhase(GamePhaseManager.Phase.SetInOrder) ||
            GamePhaseManager.Instance.IsPhase(GamePhaseManager.Phase.Shine))
        {
            canInteract = false;  // Tidak bisa berinteraksi selama fase sorting, set in order, atau shine
        }
        else
        {
            canInteract = true;  // Bisa berinteraksi di luar fase tersebut
        }

        // Jika pemain bisa berinteraksi, lakukan tindakan (misalnya sorting objek)
        if (canInteract && IsInteractingWithObject())
        {
            InteractWithObject();
        }
    }

    private bool IsInteractingWithObject()
    {
        // Implementasi untuk mendeteksi apakah pemain sedang mencoba berinteraksi dengan objek sorting
        return Input.GetButtonDown("InteractButton");  // Gantilah dengan input VR yang sesuai
    }

    private void InteractWithObject()
    {
        // Logika untuk menangani interaksi dengan objek sorting
        Debug.Log("Interaksi dengan objek sorting!");
        // Implementasi logika untuk mengubah posisi atau status objek
    }
}
