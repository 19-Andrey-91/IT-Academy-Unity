
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Scripting;

[RequireComponent(typeof(CharacterController))]
public class Character : MonoBehaviour
{
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float jumpAcceleration = 0.5f;
    [SerializeField] private float jumpTimer = 1;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private Transform viewCamera;
    [SerializeField] private Vector2 viewSensivity = new(0.2f, 0.2f);
    [SerializeField] private TouchZone touchZoneView;
    [SerializeField] private float viewClampMin = -300f;
    [SerializeField] private float viewClampMax = 300f;


    private Vector3 onlyGravityVector3 = new(0, -9.81f, 0);

    private Controls input;
    private InputAction move;
    private InputAction view;
    private InputAction jump;

    private CharacterController controller;

    private Vector2 stickDirection;
    private Vector3 moveDirection;
    private float viewAngleX;
    private float viewAngleY;

    private float timer;

    private Vector2 deltaTouch = Vector2.zero;

    private bool isGround = false;

    public CharacterController Controller { get { return controller ?? GetComponent<CharacterController>(); } }

    private void Awake()
    {
        input = new Controls();
        move = input.Touch.Move;
        view = input.Touch.View;
        jump = input.Touch.Jump;

        move.performed += Move;
        move.canceled += StopMove;
        view.performed += View;
        jump.started += Jump;
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
        touchZoneView.TouchDown += TouchView;
        moveDirection = onlyGravityVector3;
    }

    void Update()
    {
        if (Controller != null)
        {
            MoveUpdate();

            Controller.Move(moveDirection * Time.deltaTime);
            if (timer > 0)
            {
                moveDirection.y = 0;
                moveDirection.y = Mathf.Lerp(moveDirection.y, jumpForce, jumpAcceleration);
                timer -= Time.deltaTime;
            }
            else
            {
                moveDirection.y = onlyGravityVector3.y;
            }

        }
        else throw new UnityException("CharacterController is null");
    }

    private void Move(InputAction.CallbackContext obj)
    {
        stickDirection = move.ReadValue<Vector2>();
    }

    private void MoveUpdate()
    {
        moveDirection.x = stickDirection.x * moveSpeed;
        moveDirection.z = stickDirection.y * moveSpeed;
        moveDirection = transform.TransformDirection(moveDirection);
    }

    private void StopMove(InputAction.CallbackContext obj)
    {
        moveDirection.x = 0;
        moveDirection.z = 0;
    }

    private void View(InputAction.CallbackContext obj)
    {
        deltaTouch = view.ReadValue<Vector2>();
        ViewRotation(deltaTouch);
    }

    private void TouchView(PointerEventData eventData)
    {
        deltaTouch = eventData.delta;
        ViewRotation(deltaTouch);
    }

    private void ViewRotation(Vector2 deltaTouch)
    {
        viewAngleX += deltaTouch.x * viewSensivity.x;
        viewAngleY += -deltaTouch.y * viewSensivity.y;
        viewAngleY = Mathf.Clamp(viewAngleY, viewClampMin, viewClampMax);

        viewCamera.localRotation = Quaternion.Euler(viewAngleY, 0, 0);

        transform.localRotation = Quaternion.Euler(0, viewAngleX, 0);
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        if (isGround && timer <= 0)
        {
            timer = jumpTimer;
            isGround = false;
        }
        
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Ground")
        {
            isGround = true;
        }
    }
}
