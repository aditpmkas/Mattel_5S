using UnityEngine;

public class ShineTutorial : MonoBehaviour
{
    public static ShineTutorial Instance;

    private int totalDirt = 0;
    private int cleanedDirt = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        totalDirt = GameObject.FindGameObjectsWithTag("DirtyFloor").Length;
        Debug.Log("Jumlah noda: " + totalDirt);
    }

    public void DirtCleaned()
    {
        cleanedDirt++;
        Debug.Log("Noda dibersihkan: " + cleanedDirt + "/" + totalDirt);

        if (cleanedDirt >= totalDirt)
        {
            Debug.Log("Semua noda dibersihkan!");
            TaskManager.Instance.CompleteTask(TaskType.Shine);
        }
    }
}
