using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonExit : MonoBehaviour
{
    private Button buttonExit;

    private void Start()
    {
        buttonExit = GetComponent<Button>();
        buttonExit.onClick.AddListener(()=>Application.Quit());
    }
}
