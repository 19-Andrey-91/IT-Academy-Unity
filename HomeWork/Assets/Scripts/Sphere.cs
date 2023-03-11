
using UnityEngine;

public class Sphere : Subject
{
    [SerializeField] private float minScale = 0.5f;
    [SerializeField] private float maxScale = 2f;
    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        float scaleMultiplier = Random.Range(minScale, maxScale);

        transform.localScale = Vector3.one * scaleMultiplier;
    }
}
