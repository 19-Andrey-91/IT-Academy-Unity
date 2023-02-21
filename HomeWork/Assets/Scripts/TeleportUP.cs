using System;
using UnityEngine;

public class TeleportUP : Teleport
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DoTeleport(other);
            numberFloors.Subtract();
            CallEventOnCollider();
        }
    }
}
