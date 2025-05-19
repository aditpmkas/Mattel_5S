using UnityEngine;
using System.Collections.Generic;

public class BookShelfTutorial : MonoBehaviour
{
    public bool isBookA;
    public bool isBookB;
    public bool isBookC;
    public bool isBookD;

    public int requiredCount = 2;
    private int currentCount = 0;
    private bool isComplete = false;

    private HashSet<GameObject> countedBooks = new HashSet<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (isComplete) return;

        string tag = other.gameObject.tag;
        if (IsCorrectBook(tag) && !countedBooks.Contains(other.gameObject))
        {
            countedBooks.Add(other.gameObject);
            currentCount++;

            Debug.Log($"[BookShelfTutorial] Buku masuk: {tag} ({currentCount}/{requiredCount})");

            other.transform.SetParent(transform);
            other.GetComponent<Collider>().enabled = false;

            if (currentCount >= requiredCount)
            {
                isComplete = true;
                Debug.Log($"[BookShelfTutorial] Rak {name} selesai!");
            }

            // Lapor ke ShelfManager, yang akan cek semua rak
            ShelfManager.Instance.NotifyBookPlaced();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        string tag = other.gameObject.tag;
        // hanya jika buku yang benar dan sebelumnya sudah dihitung
        if (IsCorrectBook(tag) && countedBooks.Contains(other.gameObject))
        {
            countedBooks.Remove(other.gameObject);
            currentCount = Mathf.Max(0, currentCount - 1);
            // jika sebelumnya rak sempat complete, reset flag
            if (isComplete)
            {
                isComplete = false;
                Debug.Log($"[BookShelfTutorial] Rak {name} kembali jadi incomplete (karena buku diambil)");
            }
            else
            {
                Debug.Log($"[BookShelfTutorial] Buku keluar: {tag} ({currentCount}/{requiredCount})");
            }

            // kembalikan parent dan aktifkan collider
            other.transform.SetParent(null);
            other.GetComponent<Collider>().enabled = true;

            // lapor perubahan
            ShelfManager.Instance.NotifyBookPlaced();
        }
    }

    private bool IsCorrectBook(string tag)
    {
        return (isBookA && tag == "BookA") ||
               (isBookB && tag == "BookB") ||
               (isBookC && tag == "BookC") ||
               (isBookD && tag == "BookD");
    }

    public bool IsComplete() => isComplete;
    public int GetCurrentCount() => currentCount;
}
