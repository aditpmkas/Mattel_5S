using UnityEngine;
using TMPro;

public class ShineChecker : MonoBehaviour
{
    public static ShineChecker Instance { get; private set; }

    [Header("UI Elements")]
    public TextMeshProUGUI shineCountText;
    public GameObject successPanel;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip successClip;

    private int totalDirt;
    private int cleanedDirt = 0;

    private int totalRootCause;
    private int cleanedRootCause = 0;

    private bool isMopReturned = false;
    private bool isHammerReturned = false;

    private bool hasPlayedSuccessAudio = false;

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
        CheckForCompletion();
    }

    public void RegisterRootCauseHit()
    {
        cleanedRootCause++;
        UpdateUIText();
        CheckForCompletion();
    }

    public bool IsAllTasksComplete()
    {
        return cleanedDirt >= totalDirt && cleanedRootCause >= totalRootCause;
    }

    public bool IsRootCauseComplete()
    {
        return cleanedRootCause >= totalRootCause;
    }

    public void OnMopReturned()
    {
        isMopReturned = true;
        CheckIfCanShowSuccess();
    }

    public void OnHammerReturned()
    {
        isHammerReturned = true;
        CheckIfCanShowSuccess();
    }

    private void CheckForCompletion()
    {
        if (IsAllTasksComplete())
        {
            CheckIfCanShowSuccess();
        }
    }

    private void CheckIfCanShowSuccess()
    {
        if (IsAllTasksComplete() && isMopReturned && isHammerReturned)
        {
            ShowSuccessPanel();
        }
    }

    public void ShowSuccessPanel()
    {
        if (successPanel != null && !successPanel.activeSelf)
        {
            successPanel.SetActive(true);

            if (!hasPlayedSuccessAudio && audioSource != null && successClip != null)
            {
                audioSource.PlayOneShot(successClip);
                hasPlayedSuccessAudio = true;
            }
        }
    }

    private void UpdateUIText()
    {
        if (shineCountText != null)
        {
            shineCountText.text = $"Shine : {cleanedDirt}/{totalDirt}\nAkar Masalah : {cleanedRootCause}/{totalRootCause}";
        }
    }
}
