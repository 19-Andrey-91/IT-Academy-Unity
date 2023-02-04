using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickRotation : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    [SerializeField] private Vector3 angle;

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(CameraRotation);
    }

    private void CameraRotation()
    {
        obj.transform.rotation = Quaternion.Euler(angle);
    }
}
