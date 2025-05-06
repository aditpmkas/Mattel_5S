using System.Collections;
using UnityEngine;

public class BookShelfManager : MonoBehaviour
{
    public static BookShelfManager Instance;

    private BookShelf[] allShelves;

    private void Awake()
    {
        Instance = this;
        allShelves = FindObjectsOfType<BookShelf>();  // Menemukan semua rak buku
    }

    // Fungsi untuk memeriksa apakah semua rak buku sudah penuh
    public void CheckAllShelvesComplete()
    {
        foreach (BookShelf shelf in allShelves)
        {
            if (!shelf.IsComplete())
            {
                return; // Jika ada rak yang belum lengkap, hentikan pemeriksaan
            }
        }

        Debug.Log(" Semua rak sudah benar!");
        GamePhaseManager.Instance.SetPhase(GamePhaseManager.Phase.Shine); // Lanjut ke fase berikutnya
    }
}
