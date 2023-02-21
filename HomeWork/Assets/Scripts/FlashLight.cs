using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    private Camera characterCamera;

    public Camera CharacterCamera { get { return characterCamera ??= FindObjectOfType<Camera>(); } }

    private void Update()
    {
        transform.rotation = CharacterCamera.transform.rotation;
    }
}
