
using UnityEngine;

public abstract class Subject : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody rigidbodySubject;

    private Vector3 direction = Vector3.zero;

    protected virtual void Start()
    {
        rigidbodySubject =  GetComponent<Rigidbody>();
        RandomDirection();
    }

    private void FixedUpdate()
    {
        rigidbodySubject.velocity = direction * moveSpeed;
    }

    private void RandomDirection()
    {
        direction.x = Random.Range(0f, 1f);
        direction.z = Random.Range(0f, 1f);
        direction = direction.normalized;
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            direction = Vector3.Reflect(direction, collision.contacts[0].normal);
            direction = direction.normalized;
        }
    }
}
