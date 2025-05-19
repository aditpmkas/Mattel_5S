using UnityEngine;

public class PuddleHealthM2 : MonoBehaviour
{
    [Tooltip("Berapa kali sapuan agar puddle hilang")]
    public int maxSwipes = 3;

    private int currentSwipes = 0;

    public void AddSwipe()
    {
        currentSwipes++;
        Debug.Log($"Puddle swiped {currentSwipes} times");

        if (currentSwipes >= maxSwipes)
        {
            // Notify ShineTutorial dan hancurkan
            if (ShineTutorialM2.Instance != null)
                ShineTutorialM2.Instance.DirtCleaned();
            else
                Debug.LogWarning("ShineTutorial.Instance is null!");

            Destroy(gameObject);
        }
    }
}
