using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuLogic : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] List<GameObject> objectOn;
    [SerializeField] List<GameObject> objectOFF;
    [SerializeField] TextMeshProUGUI nameFolder;
    [SerializeField] string newNameFolder;
    void Start()
    {
        button.onClick.AddListener(OnClickButton);
    }

    void OnClickButton()
    {
        if (objectOFF != null)
        {
            foreach (GameObject offObject in objectOFF)
            {
                offObject.gameObject.SetActive(false);
            }
        }
        if (objectOn != null)
        {
            foreach (GameObject onObject in objectOn)
            {
                onObject.gameObject.SetActive(true);
            }
        }

        nameFolder.text = newNameFolder;
    }
}
