using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookShelfTutorial : MonoBehaviour
{
    [Header("Checklist jenis buku yang diterima oleh lemari ini")]
    public bool isBookA;
    public bool isBookB;
    public bool isBookC;
    public bool isBookD;

    public int requiredCount = 2; // jumlah buku yang harus benar untuk rak ini
    private int currentCount = 0;
    private bool isComplete = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isComplete) return;

        string tag = other.gameObject.tag;

        if (IsCorrectBook(tag))
        {
            currentCount++;
            Debug.Log("Buku benar dimasukkan ke rak: " + tag);

            other.transform.SetParent(this.transform); // Snap buku ke rak
            other.GetComponent<Rigidbody>().isKinematic = true; // opsional: biar gak jatuh
            other.GetComponent<Collider>().enabled = false; // opsional: tidak bisa diambil lagi

            if (currentCount >= requiredCount)
            {
                isComplete = true;
                Debug.Log("Rak ini sudah penuh dengan buku yang benar!");

                BookShelfManager.Instance.CheckAllShelvesComplete();
            }
        }
        else
        {
            Debug.Log("Buku salah! Tidak cocok dengan rak ini.");
        }
    }

    private bool IsCorrectBook(string tag)
    {
        return (isBookA && tag == "BookA") ||
               (isBookB && tag == "BookB") ||
               (isBookC && tag == "BookC") ||
               (isBookD && tag == "BookD");
    }

    public bool IsComplete()
    {
        return isComplete;
    }
}
