
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Character : MonoBehaviour
{
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpSpeed = 7;
    [SerializeField] private float movementSpeed = 2.0f;
    [SerializeField] private float sprintSpeed = 5.0f;
    [SerializeField] private float rotationSpeed = 0.2f;
    [SerializeField] private float animationBlendSpeed = 0.2f;
    [SerializeField] private TextMeshProUGUI textWhenDied;

    private InputAction move;
    private InputAction sprint;
    private InputAction jump;
    private InputAction onControl;
    private InputAction death;
    private InputAction hit;
    private CharacterController controller;
    private Camera characterCamera;
    private Animator animator;
    private PlayerControls playerControls;
    private float rotationAngle = 0.0f;
    private float targetAnimationSpeed = 0.0f;
    private float currentSpeed = 0f;

    private bool isJumping = false;
    private float speedY = 0.0f;

    private bool isDead = false;
    private bool isHit = false;

    private Vector3 movement = Vector3.zero;
    private Vector3 rotatedMovement = Vector3.zero;
    public CharacterController Controller { get { return controller ??= GetComponent<CharacterController>(); } }

    public Camera CharacterCamera { get { return characterCamera ??= FindObjectOfType<Camera>(); } }

    public Animator CharacterAnimator { get { return animator ??= GetComponent<Animator>(); } }

    private void Awake()
    {
        playerControls = new PlayerControls();
        move = playerControls.Player.Move;
        sprint = playerControls.Player.Sprint;
        jump = playerControls.Player.Jump;
        onControl = playerControls.Player.OnControl;
        death = playerControls.Player.Death;
        hit = playerControls.Player.Hit;
    }

    private void OnEnable()
    {
        playerControls.Enable();
        death.performed += PressTab;
        onControl.performed += PressEnter;

    }

    private void OnDisable()
    {
        hit.performed -= Hit;
        death.performed -= PressTab;
        onControl.performed -= PressEnter;
        jump.started -= Jump;
        sprint.canceled -= OffSprint;
        sprint.performed -= Sprint;
        move.performed -= Move;
        move.canceled -= StopMove;
        playerControls.Disable();
    }

    void Update()
    {
        if (!Controller.isGrounded)
        {
            speedY += gravity * Time.deltaTime;
        }
        else if (speedY < -0.1f)
        {
            speedY = -0.1f;
        }

        CharacterAnimator.SetFloat("SpeedY", speedY / jumpSpeed);
        if (isJumping && speedY < 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f, LayerMask.GetMask("Default")))
            {
                isJumping = false;
                CharacterAnimator.SetTrigger("Land");
            }
        }

        targetAnimationSpeed = SpeedForAnimation();

        Vector3 verticalMovement = Vector3.up * speedY;
        rotatedMovement = Quaternion.Euler(0.0f, CharacterCamera.transform.rotation.eulerAngles.y, 0.0f) * movement;

        Controller.Move((verticalMovement + rotatedMovement * currentSpeed) * Time.deltaTime);
        if (move.inProgress)
        {
            rotationAngle = Mathf.Atan2(rotatedMovement.x, rotatedMovement.z) * Mathf.Rad2Deg;
        }
        CharacterAnimator.SetFloat("Speed", Mathf.Lerp(CharacterAnimator.GetFloat("Speed"), targetAnimationSpeed, animationBlendSpeed));
        Quaternion currentRotation = Controller.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0.0f, rotationAngle, 0.0f);
        Controller.transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, rotationSpeed);
    }

    private void Move(InputAction.CallbackContext obj)
    {
        Vector2 moveDirection = obj.ReadValue<Vector2>();
        movement.x = moveDirection.x;
        movement.z = moveDirection.y;
        currentSpeed = sprint.inProgress ? sprintSpeed : movementSpeed;
    }

    private void StopMove(InputAction.CallbackContext obj)
    {
        movement = Vector3.zero;
        currentSpeed = 0.0f;
    }

    private void Sprint(InputAction.CallbackContext obj)
    {
        if (move.inProgress)
        {
            currentSpeed = sprintSpeed;
        }
    }

    private void OffSprint(InputAction.CallbackContext obj)
    {
        if (move.inProgress)
        {
            currentSpeed = movementSpeed;
        }
        else
        {
            currentSpeed = 0.0f;
        }
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        if (!isJumping)
        {
            isJumping = true;
            CharacterAnimator.SetTrigger("Jump");
            speedY += jumpSpeed;
        }
    }

    private float SpeedForAnimation()
    {
        if (currentSpeed > movementSpeed)
        {
            return 1f;
        }
        else if (currentSpeed == movementSpeed)
        {
            return 0.5f;
        }
        else
        {
            return 0f;
        }
    }

    private void SubscribeToButtons()
    {
        hit.performed += Hit;
        jump.started += Jump;
        move.performed += Move;
        move.canceled += StopMove;
        sprint.performed += Sprint;
        sprint.canceled += OffSprint;
    }

    private void UnsubscribeToButtons()
    {
        hit.performed -= Hit;
        jump.started -= Jump;
        sprint.canceled -= OffSprint;
        sprint.performed -= Sprint;
        move.performed -= Move;
        move.canceled -= StopMove;

    }

    private void PressEnter(InputAction.CallbackContext obj)
    {
        if (isDead)
        {
            CharacterAnimator.SetBool("Spawn", true);
            isDead = false;
            textWhenDied.text = "";
        }
    }

    private void PressTab(InputAction.CallbackContext obj)
    {
        if (!isDead)
        {
            movement = Vector3.zero;
            currentSpeed = 0f;
            CharacterAnimator.SetTrigger("IsDead");
            CharacterAnimator.SetBool("Spawn", false);
            isDead = true;
            UnsubscribeToButtons();
        }
    }

    private void Hit(InputAction.CallbackContext obj)
    {
        if (!isHit)
        {
            CharacterAnimator.SetInteger("NumHit", Random.Range(0, 3));
            CharacterAnimator.SetTrigger("Hit");
        }
    }

    private bool? ConvertIntToBool(int value)
    {
        if (value == 0)
        {
            return false;
        }
        else if (value == 1)
        {
            return true;
        }
        else
        {
            return null;
        }
    }

    public void IsSpawn(int num)
    {
        if (num == 1)
        {
            UnsubscribeToButtons();
        }
        else if (num == 0)
        {
            SubscribeToButtons();
        }
    }

    public void IsHit(int isHit)
    {
        this.isHit = ConvertIntToBool(isHit) ?? false;
    }

    public void IsDead()
    {
        textWhenDied.text = "You DIE ... \n" +
            "Press ENTER to respawn";
        isDead = true;
    }
}
