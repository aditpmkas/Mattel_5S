using UnityEngine;
using BNG;
using System.Collections;

public class ReturnMopZone2 : MonoBehaviour
{
    public string returnMessage = "Mop returned!";
    public float completeDelay = 2f;
    private bool hasCompleted = false;

    private void OnTriggerEnter(Collider other)
    {
        // **Only** the MopTool should complete Shine here
        if (!hasCompleted
           && other.CompareTag("Mop")
           && other.GetComponent<SnapTutorial>() != null)
        {
            StartCoroutine(HandleComplete(other));
        }
    }

    private IEnumerator HandleComplete(Collider other)
    {
        yield return new WaitForSeconds(completeDelay);

        if (other.CompareTag("Mop"))
        {
            // Beri tahu TaskManager (jika masih perlu)
            TaskManagerM2.Instance.CompleteTask(TaskType2.Shine);

            hasCompleted = true;
            Debug.Log(returnMessage);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Reset the subtask only if the mop itself leaves
        if (hasCompleted && other.CompareTag("Mop"))
        {
            TaskManagerM2.Instance.ResetTask(TaskType2.Shine);
            hasCompleted = false;
            Debug.Log("Mop left zone → reset Shine subtask.");
        }
    }
}
