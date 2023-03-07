using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

[RequireComponent(typeof(Rigidbody2D))]

public class Character : MonoBehaviour
{
    public static event Func<bool> OnIsGround;

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float jumpForce = 5f;

    private CharacterInputController inputController;
    private Rigidbody2D rigidbodyCharacter;
    private Vector2 directionMove = Vector2.zero;

    private void Awake()
    {
        inputController = new CharacterInputController();
        rigidbodyCharacter = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (CinemachineCore.Instance.VirtualCameraCount > 0)
        {
            CinemachineCore.Instance.GetVirtualCamera(0).Follow = transform;
        }
        else throw new UnityException("CinemachineVirtualCamera is not found");
    }

    private void OnEnable()
    {
        inputController.Enable();
        inputController.Controls.Move.performed += Move;
        inputController.Controls.Move.canceled += StopMove;
        inputController.Controls.Jump.started += Jump;
    }

    private void OnDisable()
    {
        inputController.Controls.Move.canceled -= StopMove;
        inputController.Controls.Move.performed -= Move;
        inputController.Controls.Jump.started -= Jump;
        inputController.Disable();
    }

    void FixedUpdate()
    {
        directionMove.y = rigidbodyCharacter.velocity.y;
        rigidbodyCharacter.velocity = directionMove;
    }

    private void Move(InputAction.CallbackContext obj)
    {
        directionMove.x = obj.ReadValue<float>() * moveSpeed;
    }

    private void StopMove(InputAction.CallbackContext obj)
    {
        directionMove.x = 0;
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        if (OnIsGround?.Invoke() ?? false) 
        {
            rigidbodyCharacter.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
