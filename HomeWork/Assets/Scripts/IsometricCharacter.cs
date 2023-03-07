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
    CalculatingValuesForAnimation animationMove;

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
        animationMove = new CalculatingValuesForAnimation();
    }

    void FixedUpdate()
    {
        rigidbodyCharacter.velocity = directionMove;
    }

    private void Move(InputAction.CallbackContext obj)
    {
        directionMove = obj.ReadValue<Vector2>();
        animator.SetInteger("InputX", animationMove.CalculateAnimationMove(directionMove.x));
        animator.SetInteger("InputY", animationMove.CalculateAnimationMove(directionMove.y));
        directionMove *= moveSpeed;
    }

    private void StopMove(InputAction.CallbackContext obj)
    {
        directionMove = Vector2.zero;
        animator.SetInteger("InputX", 0);
        animator.SetInteger("InputY", 0);
    }
}
