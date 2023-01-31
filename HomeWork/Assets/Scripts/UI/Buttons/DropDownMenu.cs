using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DropDownMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI changeText;

    public void InputMenu(int value)
    {
        switch (value)
        {
            case 0:
                changeText.text = "Option A";
                break;
            case 1:
                changeText.text = "Option B";
                break;
            case 2:
                changeText.text = "Option C";
                break;
            case 3:
                changeText.text = "Option B";
                break;
        }
    }

}
