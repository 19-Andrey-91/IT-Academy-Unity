using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToggleLogic : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI whatChange;
    Toggle toggle;
    void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(RenameTextMeshToNameToggle);
    }

    private void RenameTextMeshToNameToggle(bool call)
    {
        if (call)
        {
            whatChange.text = toggle.name;
        }
    }
}
