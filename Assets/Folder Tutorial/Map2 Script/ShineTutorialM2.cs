using UnityEngine;

public class ShineTutorialM2 : MonoBehaviour
{
    public static ShineTutorialM2 Instance;

    [Header("Return Zone Settings")]
    public GameObject returnZoneTrigger;

    [Header("Hammer Return Settings")]
    public GameObject hammerReturnTrigger;

    [Header("Trash Return Settings")]
    public GameObject trashReturnTrigger;

    private int totalCracks;
    private int cracksFixed = 0;

    private int totalDirt;
    private int cleanedDirt = 0;

    private int totalTrashItems;
    private int trashItemsReturned = 0;

    public bool hammerReturned { get; private set; }
    private bool trashReturned = false;
    private bool returnZoneEnabled = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        totalCracks = GameObject.FindGameObjectsWithTag("Crack").Length;
        totalDirt = GameObject.FindGameObjectsWithTag("DirtyFloor").Length;
        totalTrashItems = GameObject.FindGameObjectsWithTag("TrashItem").Length;

        foreach (var puddle in GameObject.FindGameObjectsWithTag("DirtyFloor"))
            SetCollider(puddle, false);

        SetZone(hammerReturnTrigger, false);
        SetZone(trashReturnTrigger, true);
        SetZone(returnZoneTrigger, false);

        Debug.Log($"[ShineTutorial] Cracks={totalCracks}, Dirt={totalDirt}, TrashItems={totalTrashItems}");
    }

    private void SetZone(GameObject zone, bool enabled)
    {
        if (zone == null) return;
        var col = zone.GetComponent<Collider>(); if (col) col.enabled = enabled;
        var mb = zone.GetComponent<MonoBehaviour>(); if (mb) mb.enabled = enabled;
    }

    private void SetCollider(GameObject obj, bool enabled)
    {
        var col = obj.GetComponent<Collider>(); if (col) col.enabled = enabled;
    }

    public void HammerReturned()
    {
        if (!hammerReturned)
        {
            hammerReturned = true;
            Debug.Log("[ShineTutorial] Hammer returned.");
            TryUnlockPuddles();
        }
    }

    public void ResetHammerReturned()
    {
        if (hammerReturned)
        {
            hammerReturned = false;
            Debug.Log("[ShineTutorial] Hammer taken back → hammerReturned = false");
        }
    }

    public void TrashItemReturned()
    {
        trashItemsReturned++;
        Debug.Log($"[ShineTutorial] Trash returned: {trashItemsReturned}/{totalTrashItems}");
        if (trashItemsReturned >= totalTrashItems && !trashReturned)
        {
            trashReturned = true;
            Debug.Log("[ShineTutorial] All trash returned.");
            TryUnlockPuddles();
        }
    }

    private void TryUnlockPuddles()
    {
        if (cracksFixed >= totalCracks && hammerReturned && trashReturned)
        {
            Debug.Log("[ShineTutorial] Unlocking puddles.");
            foreach (var p in GameObject.FindGameObjectsWithTag("DirtyFloor"))
                SetCollider(p, true);
        }
    }

    public void CrackFixed()
    {
        cracksFixed++;
        Debug.Log($"[ShineTutorial] Crack fixed: {cracksFixed}/{totalCracks}");
        if (cracksFixed >= totalCracks)
        {
            Debug.Log("[ShineTutorial] Cracks done → enabling return zones.");
            SetZone(hammerReturnTrigger, true);
            TryUnlockPuddles();
        }
    }

    public void DirtCleaned()
    {
        if (cracksFixed < totalCracks)
        {
            Debug.LogWarning("Cannot clean before cracks are fixed.");
            return;
        }
        cleanedDirt++;
        Debug.Log($"[ShineTutorial] Dirt cleaned: {cleanedDirt}/{totalDirt}");
        if (cleanedDirt >= totalDirt && !returnZoneEnabled)
        {
            SetZone(returnZoneTrigger, true);
            returnZoneEnabled = true;
            Debug.Log("[ShineTutorial] All dirt cleaned → final return zone active.");
        }
    }
}