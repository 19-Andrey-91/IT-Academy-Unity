using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float forceMove;

    Vector2 moveDirection = Vector2.zero;
    private Rigidbody2D rigidbodyEnemy;

    private void Start()
    {
        rigidbodyEnemy = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rigidbodyEnemy.AddForce(moveDirection);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (moveDirection == Vector2.zero)
        {
            moveDirection.x = forceMove;
        }
        else
        {
            moveDirection *= -1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
