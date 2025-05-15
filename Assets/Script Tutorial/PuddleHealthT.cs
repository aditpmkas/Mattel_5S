using UnityEngine;

public class PuddleHealthT : MonoBehaviour
{
    public static int totalPuddles = 0;
    private static int destroyedPuddles = 0;

    public int maxSwipes = 3;
    private int currentSwipes = 0;

    private void Start()
    {
        totalPuddles++;
    }

    public static void ResetPuddleCounter()
    {
        totalPuddles = 0;
        destroyedPuddles = 0;
    }

    public void AddSwipe()
    {
        currentSwipes++;
        Debug.Log("Puddle swiped " + currentSwipes + " times");

        if (currentSwipes >= maxSwipes)
        {
            destroyedPuddles++;
            Debug.Log($"Puddle destroyed. {destroyedPuddles}/{totalPuddles}");

            if (destroyedPuddles >= totalPuddles)
            {
                Debug.Log("All puddles cleaned! Shine task complete.");
                TaskManager.Instance.CompleteTask(TaskType.Shine);
            }

            Destroy(gameObject);
        }
    }
}
