using UnityEngine;

public class VisualController : MonoBehaviour
{
    public GameObject controllerHintUI; // Assign "ControllerHintUI" di Inspector

    void Start()
    {
        if (controllerHintUI != null)
        {
            controllerHintUI.SetActive(true); // Tampilkan UI saat start
        }
    }

    // Fungsi ini dipanggil dari tombol Close di UI
    public void CloseHintUI()
    {
        if (controllerHintUI != null)
        {
            Destroy(controllerHintUI); // Atau bisa gunakan SetActive(false) jika tidak ingin menghancurkan
        }
    }
}
