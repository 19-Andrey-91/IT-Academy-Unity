using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTwo : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] TextMeshProUGUI textMeshProUGUI;

    void Start()
    {
        button.onClick.AddListener(OnClickButton);
    }

    private void OnClickButton()
    {
        textMeshProUGUI.text = "Two Clicked";
    }
}
