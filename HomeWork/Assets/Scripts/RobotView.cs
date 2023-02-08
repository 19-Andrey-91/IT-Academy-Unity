using UnityEngine;
using UnityEngine.InputSystem;

public class RobotView : MonoBehaviour
{
    [SerializeField] private Transform roboCamera;
    [SerializeField] private Vector2 sensitivity = new Vector2(1, 1);

    private Controls controls;
    private InputAction inputCamera;
    private Vector2 cameraDirection;

    private float mouseX;
    private float mouseY;

    private float viewClampMin = -300f;
    private float viewClampMax = 300f;

    private void Awake()
    {
        controls = new Controls();
        inputCamera = controls.Main.Camera;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Update()
    {
        MouseMove();
    }

    private void MouseMove()
    {
        if (inputCamera.IsPressed())
        {
            cameraDirection = inputCamera.ReadValue<Vector2>();
            mouseX += cameraDirection.x * sensitivity.x * Time.deltaTime;
            mouseY += cameraDirection.y * sensitivity.y * Time.deltaTime;
            mouseY = Mathf.Clamp(mouseY, viewClampMin, viewClampMax);

            Vector3 cameraPosition = new Vector3(roboCamera.position.x, roboCamera.position.y);
            Vector3 nextCameraPosition = new Vector3(mouseY, roboCamera.position.y);
            roboCamera.localRotation = Quaternion.Euler(Vector3.Lerp(cameraPosition, nextCameraPosition, 0.1f));

            Vector3 robotPosition = new Vector3(0, transform.position.y);
            Vector3 nextRobotPosition = new Vector3(0, mouseX);
            transform.localRotation = Quaternion.Euler(Vector3.Lerp(robotPosition, nextRobotPosition, 0.1f));
        }
    }
}
