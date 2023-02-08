using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultAmmo : Ammo
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Target")
        {
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }

    }
}

