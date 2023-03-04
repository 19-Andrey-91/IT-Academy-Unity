using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class IsometricCharacter : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;

    private CharacterInputController inputController;
    private Rigidbody2D rigidbodyCharacter;
    private Vector2 directionMove = Vector2.zero;
    private Animator animator;

    private void Awake()
    {
        inputController = new CharacterInputController();
        rigidbodyCharacter = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        inputController.Enable();
        inputController.IsometricControls.Move.performed += Move;
        inputController.IsometricControls.Move.canceled += StopMove;
    }

    private void OnDisable()
    {
        inputController.IsometricControls.Move.canceled -= StopMove;
        inputController.IsometricControls.Move.performed -= Move;
        inputController.Disable();
    }

    private void Start()
    {
        if (CinemachineCore.Instance.VirtualCameraCount > 0)
        {
            CinemachineCore.Instance.GetVirtualCamera(0).Follow = transform;
        }
        else throw new UnityException("CinemachineVirtualCamera is not found");
    }

    void FixedUpdate()
    {
        rigidbodyCharacter.velocity = directionMove;
    }

    private void Move(InputAction.CallbackContext obj)
    {
        directionMove = obj.ReadValue<Vector2>();
        animator.SetFloat("InputX", directionMove.x);
        animator.SetFloat("InputY", directionMove.y);
        Debug.Log(obj.ReadValue<Vector2>());
        directionMove *= moveSpeed;
    }

    private void StopMove(InputAction.CallbackContext obj)
    {
        directionMove = Vector2.zero;
        animator.SetFloat("InputX", 0);
        animator.SetFloat("InputY", 0);
    }
}
