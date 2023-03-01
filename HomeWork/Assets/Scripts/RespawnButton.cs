using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RespawnButton : MonoBehaviour
{
    private Button button;

    public Button Button { get => button ??= GetComponent<Button>(); }
}
