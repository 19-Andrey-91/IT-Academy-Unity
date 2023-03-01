using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasDead : MonoBehaviour
{
    private Canvas deadUI;
    public Canvas DeadUI {  get => deadUI ??= GetComponent<Canvas>(); }
}
