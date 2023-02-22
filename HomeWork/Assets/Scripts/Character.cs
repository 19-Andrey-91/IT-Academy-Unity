
using Cinemachine;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Character : MonoBehaviour
{
    public event Action<bool> OnWalks;

    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float movementSpeed = 2.0f;

    private InputAction move;
    private CharacterController controller;
    private Camera characterCamera;
    private PlayerControls playerControls;

    private Quaternion cameraRotationY;
    private Vector3 verticalMovement;
    private Vector3 movement = Vector3.zero;
    private Vector3 rotatedMovement = Vector3.zero;
    public CharacterController Controller { get { return controller ??= GetComponent<CharacterController>(); } }

    public Camera CharacterCamera { get { return characterCamera ??= FindObjectOfType<Camera>(); } }

    private void Awake()
    {
        playerControls = new PlayerControls();
        move = playerControls.Character.Move;

    }

    private void OnEnable()
    {
        playerControls.Enable();
        move.performed += Move;
        move.canceled += StopMove;
        move.started += StartMove;
    }

    private void OnDisable()
    {
        move.started -= StartMove;
        move.performed -= Move;
        move.canceled -= StopMove;
        playerControls.Disable();
    }

    private void Start()
    {
        verticalMovement = Vector3.up * gravity;
    }

    void Update()
    {
        cameraRotationY = Quaternion.Euler(0.0f, CharacterCamera.transform.rotation.eulerAngles.y, 0.0f);
        rotatedMovement = cameraRotationY * movement;
        transform.rotation = cameraRotationY;

        Controller.Move((verticalMovement + rotatedMovement) * movementSpeed * Time.deltaTime);
    }

    private void StartMove(InputAction.CallbackContext obj)
    {
        OnWalks?.Invoke(true);
    }


    private void Move(InputAction.CallbackContext direction)
    {
        Vector2 moveDirection = direction.ReadValue<Vector2>();
        movement.x = moveDirection.x;
        movement.z = moveDirection.y;
    }

    private void StopMove(InputAction.CallbackContext obj)
    {
        movement = Vector3.zero;
        OnWalks?.Invoke(false);
    }
}