using UnityEngine;

public class BookInteraction : MonoBehaviour
{
    public GameObject book;
    private bool canInteract = true;

    private void Update()
    {
        // Cek apakah pemain boleh berinteraksi dengan buku berdasarkan fase
        if (GamePhaseManager.Instance.IsPhase(GamePhaseManager.Phase.Sorting) ||
            GamePhaseManager.Instance.IsPhase(GamePhaseManager.Phase.SetInOrder) ||
            GamePhaseManager.Instance.IsPhase(GamePhaseManager.Phase.Shine))
        {
            canInteract = false;
        }
        else
        {
            canInteract = true;
        }

        if (canInteract && IsGrabbingBook())
        {
            GrabBook();
        }
    }

    private bool IsGrabbingBook()
    {
        // Gantilah "GrabButton" dengan input VR atau input lain sesuai kebutuhan
        return Input.GetButtonDown("GrabButton");
    }

    private void GrabBook()
    {
        Debug.Log("Buku diambil!");
        // Tambahkan logika tambahan untuk ambil buku jika diperlukan
    }
}
