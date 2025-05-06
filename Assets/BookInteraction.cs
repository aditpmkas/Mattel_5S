using UnityEngine;

public class BookInteraction : MonoBehaviour
{
    public GameObject book;
    private bool canInteract = true;

    private void Update()
    {
        // Mengatur apakah pemain dapat berinteraksi dengan buku atau objek berdasarkan fase
        if (GamePhaseManager.Instance.IsPhase(GamePhaseManager.Phase.Sorting) ||
            GamePhaseManager.Instance.IsPhase(GamePhaseManager.Phase.SetInOrder) ||
            GamePhaseManager.Instance.IsPhase(GamePhaseManager.Phase.Shine))
        {
            canInteract = false;  // Menonaktifkan interaksi dengan objek selama fase-fase tersebut
        }
        else
        {
            canInteract = true;  // Mengaktifkan interaksi di luar fase tersebut
        }

        // Jika pemain bisa berinteraksi, lakukan tindakan (misalnya grab buku)
        if (canInteract && IsGrabbingBook())
        {
            GrabBook();
        }
    }

    private bool IsGrabbingBook()
    {
        // Implementasi untuk mendeteksi apakah pemain sedang mencoba mengambil buku
        // Misalnya, menggunakan sistem input VR atau trigger untuk grab
        return Input.GetButtonDown("GrabButton");  // Gantilah dengan input VR yang sesuai
    }

    private void GrabBook()
    {
        // Logika untuk menangani interaksi dengan buku
        Debug.Log("Buku diambil!");
        // Implementasi logika untuk mengambil buku atau objek
    }
}
