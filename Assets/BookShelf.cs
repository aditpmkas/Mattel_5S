using UnityEngine;

public class BookShelf : MonoBehaviour
{
    [Header("Checklist jenis buku yang diterima oleh lemari ini")]
    public bool isBookA;
    public bool isBookB;
    public bool isBookC;
    public bool isBookD;

    public int requiredCount = 2; // Jumlah buku yang harus benar untuk lemari ini
    private int currentCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (!GamePhaseManager.Instance.IsPhase(GamePhaseManager.Phase.SetInOrder)) return;

        string tag = other.gameObject.tag;

        // Cek apakah buku cocok berdasarkan checklist
        if (IsCorrectBook(tag))
        {
            currentCount++;
            Debug.Log("Correct book placed: " + tag);

            other.transform.SetParent(this.transform); // Snap ke rak

            BookShelfManager.Instance.ReportCorrectBookPlaced();
        }
        else
        {
            Debug.Log("Salah lemari! Buku ini tidak cocok dengan rak ini.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!GamePhaseManager.Instance.IsPhase(GamePhaseManager.Phase.SetInOrder)) return;

        string tag = other.gameObject.tag;

        if (IsCorrectBook(tag))
        {
            currentCount--;
            if (currentCount < 0) currentCount = 0;

            Debug.Log("Correct book removed: " + tag);
            BookShelfManager.Instance.ReportCorrectBookRemoved();
        }
    }

    private bool IsCorrectBook(string tag)
    {
        return (isBookA && tag == "BookA") ||
               (isBookB && tag == "BookB") ||
               (isBookC && tag == "BookC") ||
               (isBookD && tag == "BookD");
    }
}
