using System.Collections.Generic;
using UnityEngine;

public class BookShelf : MonoBehaviour
{
    [Header("Checklist jenis buku yang diterima oleh lemari ini")]
    public bool isBookA;
    public bool isBookB;
    public bool isBookC;
    public bool isBookD;

    public int requiredCount = 2;
    private HashSet<GameObject> placedBooks = new HashSet<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (!GamePhaseManager.Instance.IsPhase(GamePhaseManager.Phase.SetInOrder)) return;

        string tag = other.gameObject.tag;

        if (IsCorrectBook(tag))
        {
            if (!placedBooks.Contains(other.gameObject))
            {
                placedBooks.Add(other.gameObject);
                BookShelfManager.Instance.ReportCorrectBookPlaced();
                Debug.Log("Correct book placed: " + tag);
            }

            other.transform.SetParent(this.transform);
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
            if (placedBooks.Contains(other.gameObject))
            {
                placedBooks.Remove(other.gameObject);
                BookShelfManager.Instance.ReportCorrectBookRemoved();
                Debug.Log("Correct book removed: " + tag);
            }
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
