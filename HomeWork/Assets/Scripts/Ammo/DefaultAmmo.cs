using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultAmmo : Ammo
{
    internal override void Fly(GameObject newObject)
    {
        base.Fly(newObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Target")
        {
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }

    }
}

