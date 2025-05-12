using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public void GoToNextScene()
    {
        SceneManager.LoadScene("L2 M1");
    }
} 