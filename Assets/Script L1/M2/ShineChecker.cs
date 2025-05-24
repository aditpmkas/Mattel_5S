using UnityEngine;
using TMPro;

public class ShineChecker : MonoBehaviour
{
    public static ShineChecker Instance { get; private set; }

    [Header("UI Elements")]
    public TextMeshProUGUI shineCountText;
    public GameObject successPanel;

    private int totalDirt;
    private int cleanedDirt = 0;

    private int totalRootCause;
    private int cleanedRootCause = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        if (successPanel != null)
            successPanel.SetActive(false);

        totalDirt = GameObject.FindGameObjectsWithTag("DirtyFloor").Length;
        totalRootCause = GameObject.FindGameObjectsWithTag("RootCauseLevel1").Length;

        UpdateUIText();
    }

    public void RegisterShineHit()
    {
        cleanedDirt++;
        UpdateUIText();
    }

    public void RegisterRootCauseHit()
    {
        cleanedRootCause++;
        UpdateUIText();
    }

    public bool IsAllTasksComplete()
    {
        return cleanedDirt >= totalDirt && cleanedRootCause >= totalRootCause;
    }

    public bool IsRootCauseComplete()
    {
        return cleanedRootCause >= totalRootCause;
    }

    public void ShowSuccessPanel()
    {
        if (successPanel != null)
            successPanel.SetActive(true);
    }

    private void UpdateUIText()
    {
        if (shineCountText != null)
        {
            shineCountText.text = $"Shine : {cleanedDirt}/{totalDirt}\nAkar Masalah : {cleanedRootCause}/{totalRootCause}";
        }
    }
}
