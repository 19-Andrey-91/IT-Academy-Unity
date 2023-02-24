using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    public event Action<Vector3, float> OnMoving;

    [SerializeField] private float speed = 1f;

    private PlayerController controller;
    private Animator animator;

    private Vector3 directionMove = new Vector3(0, 0, 0);

    private void Awake()
    {
        controller = new PlayerController();
    }

    private void OnEnable()
    {
        controller.Enable();
        controller.Controls.Move.performed += Move;
        controller.Controls.Move.canceled += StopMove;
    }

    private void OnDisable()
    {
        controller.Controls.Move.performed -= Move;
        controller.Controls.Move.canceled -= StopMove;
        controller.Disable();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Move(InputAction.CallbackContext obj)
    {
        directionMove.x = obj.ReadValue<float>();
        animator.SetBool("Run", true);
        AnimationDirection(directionMove.x);
    }

    private void StopMove(InputAction.CallbackContext obj)
    {
        directionMove.x = 0;
        animator.SetBool("Run", false);
    }

    private void Update()
    {
        OnMoving?.Invoke(directionMove, speed);
    }

    private void AnimationDirection(float direction)
    {
        if (direction > 0)
        {
            animator.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (direction < 0)
        {
            animator.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
