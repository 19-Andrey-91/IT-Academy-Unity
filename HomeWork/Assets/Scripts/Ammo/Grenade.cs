using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : Ammo
{
    [SerializeField] private float radius = 5f;
    [SerializeField] private float forceExplosion = 100f;
    internal override void Fly(GameObject newObject)
    {
        base.Fly(newObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        Explode();
        Destroy(gameObject);
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(rb.transform.position, radius);
        foreach (Collider collider in colliders)
        {
            Rigidbody rigidbody = collider.attachedRigidbody;
            if (rigidbody)
            {
                rigidbody.AddExplosionForce(forceExplosion, rb.transform.position, radius);
            }
        }
    }
}
