using UnityEngine;

public class BookShelfTutorial : MonoBehaviour
{
    [Header("Checklist jenis buku yang diterima oleh lemari ini")]
    public bool isBookA;
    public bool isBookB;
    public bool isBookC;
    public bool isBookD;

    public int requiredCount = 2; // Jumlah buku yang harus masuk dengan benar
    private int currentCount = 0;
    private bool isComplete = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isComplete) return;

        string tag = other.gameObject.tag;
        Debug.Log($"[BookShelfTutorial] Buku masuk dengan tag: {tag}");

        if (IsCorrectBook(tag))
        {
            currentCount++;
            Debug.Log($"[BookShelfTutorial] Buku cocok! Total sekarang: {currentCount}/{requiredCount}");

            other.transform.SetParent(this.transform); // Snap ke rak
            other.GetComponent<Rigidbody>().isKinematic = true;
            other.GetComponent<Collider>().enabled = false;

            if (currentCount >= requiredCount)
            {
                isComplete = true;
                Debug.Log($"[BookShelfTutorial] RAK SELESAI! ({tag}), Total: {currentCount}");

                ShelfManager.Instance.CheckAllShelvesComplete();
            }
        }
        else
        {
            Debug.Log($"[BookShelfTutorial] Buku SALAH ({tag}), tidak cocok dengan rak ini!");
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
