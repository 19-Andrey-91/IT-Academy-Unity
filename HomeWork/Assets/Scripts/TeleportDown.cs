using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportDown : Teleport
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (numberFloors.Floor > 1)
            {
                DoTeleport(other);
                numberFloors.Add();
                CallEventOnCollider();
            }
        }
    }
}
