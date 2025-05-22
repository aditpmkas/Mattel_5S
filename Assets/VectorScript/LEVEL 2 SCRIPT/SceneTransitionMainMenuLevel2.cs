using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionMainMenuLevel2 : MonoBehaviour
{
    public void GoToLevel2Map1()
    {
        if (ProgressManagerLevel2.Instance != null)
        {
            ProgressManagerLevel2.Instance.ResetProgress();
        }
        SceneManager.LoadScene("L2 M1");
    }
    public void GoToLevel2Map2()
    {
        if (ProgressManagerLevel2Map2.Instance != null)
        {
            ProgressManagerLevel2Map2.Instance.ResetProgress();
        }
        SceneManager.LoadScene("L2 M2");
    }
}
