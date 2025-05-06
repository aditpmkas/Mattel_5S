using System.Collections;
using UnityEngine;

public class BookShelf : MonoBehaviour
{
    [Header("Checklist jenis buku yang diterima oleh lemari ini")]
    public bool isBookA;
    public bool isBookB;
    public bool isBookC;
    public bool isBookD;

    public int requiredCount = 2; // jumlah buku yang harus benar untuk lemari ini
    private int currentCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (!GamePhaseManager.Instance.IsPhase(GamePhaseManager.Phase.SetInOrder)) return;

        string tag = other.gameObject.tag;

        // Memeriksa apakah buku cocok dengan rak ini berdasarkan ceklis
        if (IsCorrectBook(tag))
        {
            currentCount++;
            Debug.Log("Correct book placed: " + tag);
            other.transform.SetParent(this.transform); // snap ke rak

            if (currentCount >= requiredCount)
            {
                Debug.Log("Lemari ini sudah penuh dengan buku yang benar!");

                // Cek semua rak
                BookShelfManager.Instance.CheckAllShelvesComplete();
            }
        }
        else
        {
            Debug.Log("Salah lemari! Buku ini tidak cocok dengan rak ini.");
        }
    }

    // Fungsi untuk memeriksa apakah buku sesuai dengan rak berdasarkan tag dan ceklis
    private bool IsCorrectBook(string tag)
    {
        return (isBookA && tag == "BookA") ||
               (isBookB && tag == "BookB") ||
               (isBookC && tag == "BookC") ||
               (isBookD && tag == "BookD");
    }

    public bool IsComplete()
    {
        return currentCount >= requiredCount;
    }
}
