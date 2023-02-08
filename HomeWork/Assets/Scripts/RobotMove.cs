using UnityEngine;
using UnityEngine.InputSystem;

public class RobotMove : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10;
    [SerializeField] private float powerJump = 1;

    Vector3 jumpDirection;
    private Rigidbody body;
    private Controls input;
    private InputAction move;
    private InputAction jump;
    private Vector2 keyboardDirection;
    private Vector3 newMoveDirection;

    private bool isGround = true;
    private bool movePressed = false;

    private void Awake()
    {
        input = new Controls();
        move = input.Main.Move;
        jump = input.Main.Jump;
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    void Start()
    {
        jumpDirection = new Vector3(0f, powerJump, 0f) * 100f;
        body = GetComponent<Rigidbody>();

        jump.performed += JumpPerformed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        CheckGround(collision, true);
    }

    private void OnCollisionExit(Collision collision)
    {
        CheckGround(collision, false);
    }

    private void JumpPerformed(InputAction.CallbackContext obj)
    {
        if (isGround)
        {
            body.AddForce(jumpDirection, ForceMode.Acceleration);
        }
    }

    private void Update()
    {
        movePressed = move.IsPressed();
    }

    private void FixedUpdate()
    {
        if (movePressed)
        {
            keyboardDirection = move.ReadValue<Vector2>() * movementSpeed;
            newMoveDirection = transform.TransformDirection(new Vector3(keyboardDirection.x, 0, keyboardDirection.y));
            body.AddForce(newMoveDirection, ForceMode.Acceleration);
        }
    }

    void CheckGround(Collision collision, bool ground)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGround = ground;
        }
    }
}
