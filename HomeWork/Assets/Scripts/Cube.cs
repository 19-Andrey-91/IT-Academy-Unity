
using UnityEngine;

public class Cube : Subject
{
    Material material;
    protected override void Start()
    {
        base.Start();
        material = GetComponent<Renderer>().material;
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        material.color = new Color(Random.value, Random.value, Random.value);
    }
}
