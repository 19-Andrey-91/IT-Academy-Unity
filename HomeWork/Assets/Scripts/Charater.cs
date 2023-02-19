
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Character : MonoBehaviour
{
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
        move = playerControls.Player.Move;
    }

    private void OnEnable()
    {
        playerControls.Enable();
        move.performed += Move;
        move.canceled += StopMove;
        playerControls.Player.Look.performed += Look;
    }

    private void OnDisable()
    {
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

        Controller.Move((verticalMovement + rotatedMovement) * movementSpeed * Time.deltaTime);
        transform.rotation = cameraRotationY;

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
    }

    private void Look(InputAction.CallbackContext obj)
    {
        Vector2 pos = obj.ReadValue<Vector2>();
        CharacterCamera.transform.rotation = Quaternion.Euler(0.0f, pos.x, pos.y);
    }
}