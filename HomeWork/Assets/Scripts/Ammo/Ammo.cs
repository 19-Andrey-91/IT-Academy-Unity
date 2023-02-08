using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] internal float force = 10f;

    [SerializeField] private float lifeTime = 10f;

    [SerializeField] private int number = 10;

    internal Rigidbody rb;
    internal Vector3 direction;

    public int Number { get => number; private set => number = value; }

    internal virtual void Fly(GameObject newObject) 
    {
        rb = newObject.GetComponent<Rigidbody>();
        direction = Camera.main.transform.TransformDirection(new Vector3(1, 1, force));
        rb.AddForce(direction, ForceMode.Impulse);
    }
    private void Awake()
    {
        AmmoDestroy();
    }
    internal virtual void AmmoDestroy()
    {
        Destroy(gameObject, lifeTime);
    }
}
