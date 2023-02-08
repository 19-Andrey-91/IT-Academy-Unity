using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour, IFire
{
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    [SerializeField] TriggerEnter triggerObject;
    private Controls controls;
    private InputAction click;
    private Trigger trigger;
    private Ammo ammo;
    private int ammoInMagazine;
    GameObject go;

    private void Awake()
    {
        controls = new Controls();
        click = controls.Main.Fire;
        click.performed += ClickFire;
        triggerObject.triggerDelegate = OnTrigger;
    }

    private void ClickFire(InputAction.CallbackContext obj)
    {
        Fire();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    public void Fire()
    {
        if (go != null)
        {
            if (ammoInMagazine > 0)
            {
                GameObject newObject = Instantiate(go, transform.position, transform.rotation);
                ammo.Fly(newObject);
                ammoInMagazine--;
                textNumberAmmo(ammoInMagazine);
            }
        }
    }

    private void textNumberAmmo(int num)
    {
        textMeshProUGUI.text = $"Ammo : {num}";
    }

    private void OnTrigger(Collider other)
    {
        trigger = other.GetComponent<Trigger>();
        ammo = trigger.GetAmmo;
        go = ammo.gameObject;
        ammoInMagazine = ammo.Number;
        textNumberAmmo(ammoInMagazine);
    }
}
