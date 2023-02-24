using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public class MoveGround : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private Character character;
    private SpriteRenderer spriteRenderer;
    private float displacementFactor = 3f;
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
            CreateNewGround(GroundsCreationSide.Left).CreatedGroundAround = true;
            CreateNewGround(GroundsCreationSide.Right).CreatedGroundAround = true;
        }
        displacementVector.x = spriteRenderer.bounds.size.x * displacementFactor;
    }
    private void Update()
    {
        if (transform.position.x > displacementVector.x / 2)
        {
            transform.position -= displacementVector;
        }
        if (transform.position.x < -displacementVector.x / 2)
        {
            transform.position += displacementVector;
        }
    }

    private void Move(Vector3 directionMove, float overallSpeed)
    {
        transform.position += -directionMove * moveSpeed * overallSpeed * Time.deltaTime;
    }

    private MoveGround CreateNewGround(GroundsCreationSide direction)
    {
        float newPositionX = 0;

        switch (direction)
        {
            case GroundsCreationSide.Left:
                newPositionX = spriteRenderer.transform.position.x - spriteRenderer.bounds.size.x;
                break;
            case GroundsCreationSide.Right:
                newPositionX = spriteRenderer.transform.position.x + spriteRenderer.bounds.size.x;
                break;
            default:
                break;
        }

        Vector3 newPosition = new Vector3(newPositionX, 0f, 0f);
        MoveGround newGround = Instantiate(this, newPosition, transform.rotation, transform.parent);
        return newGround;
    }

    private enum GroundsCreationSide
    {
        Left,
        Right,
    }
}
