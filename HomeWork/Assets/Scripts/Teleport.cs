using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour, IOnCollider
{
    public event Action OnCollider;

    [SerializeField] private float deltaTeleportY ;
    [SerializeField] protected NumberFloors numberFloors;

    protected void DoTeleport(Collider other)
    {
        CharacterController controller = other.GetComponent<CharacterController>();
        controller.enabled = false;
        other.gameObject.transform.Translate(0f, deltaTeleportY, 0f, Space.World);
        controller.enabled = true;
    }

    protected void CallEventOnCollider()
    {
        OnCollider?.Invoke();
    }
}
