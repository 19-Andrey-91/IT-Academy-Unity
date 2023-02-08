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
    private void OnCollisionEnter(Collision collision)
    {
        Explode();
        Destroy(GameObject.Find("Grenade(Clone)"));
    }
    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider collider in colliders)
        {
            Rigidbody rigidbody = collider.attachedRigidbody;
            if (rigidbody)
            {
                rigidbody.AddExplosionForce(forceExplosion, transform.position, radius);
            }
        }
    }
}
