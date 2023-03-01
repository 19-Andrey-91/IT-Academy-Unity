using UnityEngine;
using SpaceCharacterStateMachine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.UI;
using Cinemachine;

[RequireComponent(typeof(CharacterStateMachine))]
public class Character : MonoBehaviour
{
    public event Action<float> OnMoving;

    [SerializeField] private GroundCheck groundCheck;
    [SerializeField] private float forceJump = 1f;
    [SerializeField] private float moveSpeed = 1f;

    private Canvas deadUI;
    private Button restartBotton;
    private CinemachineVirtualCamera cinemachine;

    private CharacterStateMachine characterStateMachine;
    private InputCharacterController controller;

    private Vector2 spawnPosition;

    public Canvas DeadUI { get => deadUI ??= FindObjectOfType<CanvasDead>().DeadUI; }
    public float MoveSpeed { get => moveSpeed; private set => _ = moveSpeed; }
    public float ForceJump { get => forceJump; private set => _ = forceJump; }
    public GroundCheck GroundCheck { get => groundCheck; }
    public Vector2 SpawnPosition { get => spawnPosition; }

    private void Awake()
    {
        restartBotton = FindObjectOfType<RespawnButton>().Button;
        characterStateMachine = GetComponent<CharacterStateMachine>();
        controller = new InputCharacterController();
    }

    private void OnEnable()
    {
        restartBotton.onClick.AddListener(Respawn);
        controller.Enable();
        controller.Character.Move.performed += Move;
        controller.Character.Move.canceled += StopMove;
        controller.Character.Jump.started += Jump;
    }

    private void OnDisable()
    {
        controller.Character.Move.performed -= Move;
        controller.Character.Move.canceled -= StopMove;
        controller.Character.Jump.started -= Jump;
        controller.Disable();
    }
    private void Start()
    {
        cinemachine = FindObjectOfType<CinemachineVirtualCamera>();
        cinemachine.Follow = transform;
        spawnPosition = transform.position;
    }

    private void Move(InputAction.CallbackContext obj)
    {
        OnMoving?.Invoke(obj.ReadValue<float>());
        characterStateMachine.SetStateMove();
    }

    private void StopMove(InputAction.CallbackContext obj)
    {
        OnMoving?.Invoke(0);
        characterStateMachine.SetStateIdle();
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        characterStateMachine.SetStateJump();
        if(controller.Character.Move.IsPressed())
        {
            characterStateMachine.SetStateMove();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            characterStateMachine.SetStateDead();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Death"))
        {
            characterStateMachine.SetStateDead();
        }
    }

    private void Respawn()
    {
        characterStateMachine.SetStateRespawn();
    }
}
