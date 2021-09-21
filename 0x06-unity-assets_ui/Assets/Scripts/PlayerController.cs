using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputMaster inputMaster;
    public CharacterController controller;
    public Transform cam;
    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public LayerMask groundMask; // Sphere that collides with the ground

    public float speed = 6f;
    public float gravity = -19.62f;
    public float turnSmoothTime = 0.1f;
    public float jumpHeight = 3f;
    float turnSmoothVelocity; // Smooth movement of the camera
    Vector3 velocity; // helps with gravity
    bool isGrounded; // Stores if the player is grounded
    Vector3 startPos = new Vector3(0f, 20f, 0f);

    void Awake ()
    {
        inputMaster = new InputMaster();
    }

    private void OnEnable()
    {
        inputMaster.Enable();
    }
    private void OnDisable()
    {
        inputMaster.Disable();
    }

    void Update()
    {
        Cursor.visible = false;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); // Checks if player is on the ground
        if (isGrounded && velocity.y < 0)
            velocity.y = -9.81f;

        Vector2 movement = inputMaster.Player.Move.ReadValue<Vector2>();
        Vector3 move = new Vector3(movement.x, 0f, movement.y).normalized;
        if (move.magnitude >= 0.1f) // Move
        {
            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        if (inputMaster.Player.Jump.triggered && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        if (transform.position.y < -40) // Reset position
        {
            controller.Move(new Vector3(-transform.position.x - 25f, 10, -transform.position.z));
            controller.Move(new Vector3(25f, 50f, 0f));
            velocity.y = -9.81f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime); // Jumps
    }

}
