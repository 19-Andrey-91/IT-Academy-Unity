
using System.Collections;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public class MoveGround : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private Character character;
    private SpriteRenderer spriteRenderer;
    private float displacementFactor = 1.5f;
    private Vector3 displacementVector = Vector3.zero;

    private bool createdGroundAround = false;

    public bool CreatedGroundAround { get => createdGroundAround; set => createdGroundAround = value; }

    private void Awake()
    {
        character = FindObjectOfType<Character>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        character.OnMoving += Move;
    }

    private void OnDisable()
    {
        character.OnMoving -= Move;
    }

    private void Start()
    {
        if (!createdGroundAround)
        {
            CreateNewGround(ToCreate.Left).CreatedGroundAround = true;
            CreateNewGround(ToCreate.Right).CreatedGroundAround = true;
        }
        displacementVector.x = spriteRenderer.bounds.size.x * displacementFactor * 2;
    }
    private void Update()
    {
        if (transform.position.x > spriteRenderer.bounds.size.x * displacementFactor)
        {
            transform.position -= displacementVector;
        }
        if (transform.position.x < -spriteRenderer.bounds.size.x * displacementFactor)
        {
            transform.position += displacementVector;
        }
    }

    private void Move(Vector3 directionMove)
    {
        transform.position += -directionMove * moveSpeed * Time.deltaTime;
    }

    private MoveGround CreateNewGround(ToCreate direction)
    {
        float newPositionX = 0;

        switch (direction)
        {
            case ToCreate.Left:
                newPositionX = spriteRenderer.transform.position.x - spriteRenderer.bounds.size.x;
                break;
            case ToCreate.Right:
                newPositionX = spriteRenderer.transform.position.x + spriteRenderer.bounds.size.x;
                break;
            default:
                break;
        }

        Vector3 newPosition = new Vector3(newPositionX, 0f, 0f);
        MoveGround newGround = Instantiate(this, newPosition, transform.rotation, transform.parent);
        return newGround;
    }

    private enum ToCreate
    {
        Left,
        Right,
    }
}
