using System;
using UnityEngine;

public class TeleportUP : Teleport, IOnCollider
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
