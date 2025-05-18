using UnityEngine;

public class MapTeleport : MonoBehaviour
{
    public Transform playerRig;

    public void GoToMap1()
    {
        playerRig.position = new Vector3(-14.775f, -13.653f, 14.838f);
    }

    public void GoToMap2()
    {
        playerRig.position = new Vector3(-21.82f, -13.653f, 14.97f);
    }
}
