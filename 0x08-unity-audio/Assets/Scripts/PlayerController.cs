using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    private InputMaster inputMaster;
    public CharacterController controller;
    public Transform cam, groundCheck;
    public float groundDistance = 0.1f;
    public LayerMask groundMask; // Sphere that collides with the ground
    public float speed = 6f, gravity = -19.62f, turnSmoothTime = 0.1f, jumpHeight = 3f;
    public bool isInverted = false, isGrounded; // Stores if the player is grounded
    public AudioSource grass, bgm, victory;
    float turnSmoothVelocity; // Smooth movement of the camera
    Vector3 startPos = new Vector3(0f, 20f, 0f), velocity; // velocity helps with gravity;
    int coins;

    void Awake ()
    {
        inputMaster = new InputMaster();
        bgm.Play();
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
        if (PlayerPrefs.GetInt("paused") == 0 || Time.timeScale == 0)
            Cursor.visible = false;
        else
            Cursor.visible = true;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); // Checks if player is on the ground
        if (isGrounded && velocity.y < 0)
            velocity.y = -9.81f;

        Vector2 movement = inputMaster.Player.Move.ReadValue<Vector2>();
        Vector3 move = new Vector3(movement.x, 0f, movement.y).normalized;
        if (move.magnitude >= 0.1f) // Move
        {
            if (!grass.isPlaying && isGrounded)
                grass.Play();
            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        if (move.magnitude >= 0.1f && !isGrounded || move.magnitude == 0)
            grass.Stop();
        if (inputMaster.Player.Jump.triggered && isGrounded)
        {
            if (grass.isPlaying)
                grass.Stop();
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime); // Jumps

        if (transform.position.y < -40) // Reset position
        {
            this.gameObject.GetComponent<CharacterController>().enabled = false;
            this.gameObject.transform.position = new Vector3(0, 20, 0);
            velocity.y = -9.81f;
            this.gameObject.GetComponent<CharacterController>().enabled = true;

        }
        if (coins == 8)
        {
            GameObject.Find("Plat (16)").transform.localScale = new Vector3 (4, 1, 1);
            GameObject.Find("Instruction").GetComponent<TextMesh>().text = "Well done!";
            GameObject.Find("TextPumpkin").SetActive(false);
            coins++;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            coins++;
            Debug.Log(coins);
            other.gameObject.SetActive(false);
        }
        if (other.name == "WinFlag")
        {
            bgm.Pause();
            victory.Play();
            Cursor.visible = true;
        }
    }
}
