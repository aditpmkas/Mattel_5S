using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialUI : MonoBehaviour
{
    public TextMeshProUGUI taskTitleText;
    public TextMeshProUGUI taskInstructionText;

    public void UpdateUI(string title, string instruction)
    {
        if (taskTitleText != null) taskTitleText.text = title;
        if (taskInstructionText != null) taskInstructionText.text = instruction;
    }
}
