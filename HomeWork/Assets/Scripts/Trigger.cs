using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] private Ammo ammo;

    public Ammo GetAmmo { get => ammo; private set => ammo = value; }
}
