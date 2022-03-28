using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    InputMaster inputMaster;
    CharacterController characterController;
    Animator animator;
    public Transform mainCamera;
    public AudioSource footsteps;
    public AudioClip[] footstepsClips;

    float turnSmoothVelocity, turnSmoothTime = 0.1f;

    // variable to store optimized setter/getter parameter IDs
    int isWalkingHash;
    int isRunningHash;
    int isJumpingHash;
    int isFallingHash;

    // Input Values
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;
    Vector3 appliedMovement;
    bool isMovementPressed;
    bool isRunPressed;

    // Constants
    public float runMultiplier = 6.0f, gravity = -9.81f, groundedGravity = -9.81f;

    // Jumping Variables
    bool isJumpPressed = false;
    float initialJumpVelocity;
    float initialJumpPosition;
    public float maxJumpHeight = 1.5f;
    public float maxJumpTime = 0.5f;
    bool isJumping = false;
    bool isJumpAnimating;
    Vector3 velocity;

    // State Machine Variables
    PlayerBaseState currentState;
    PlayerStateFactory states;

    //getters/setters
    public AudioSource Footsteps { get { return footsteps; }  set { footsteps = value; } }
    public AudioClip[] FootstepsClips { get { return footstepsClips; } }
    public CharacterController CharacterController { get { return characterController; } }
    public PlayerBaseState CurrentState { get { return currentState; } set { currentState = value; } }
    public Animator Animator { get { return animator; } }
    public Vector2 CurrentMovementInput { get { return currentMovementInput; } }
    public float InitialJumpVelocity { get { return initialJumpVelocity; } }
    public float CurrentMovementY { get { return currentMovement.y;} set { currentMovement.y = value; } }
    public float AppliedMovementY { get { return appliedMovement.y; } set { appliedMovement.y = value; } }
    public float AppliedMovementX { get { return appliedMovement.x; } set { appliedMovement.x = value; } }
    public float AppliedMovementZ { get { return appliedMovement.z; } set { appliedMovement.z = value; } }
    public float RunMultiplier { get { return runMultiplier; } }
    public float GroundedGravity { get { return groundedGravity; } }
    public float Gravity { get { return gravity; } }
    public float InitialJumpPosition { get { return initialJumpPosition; } set { initialJumpPosition = value; } }
    public bool IsMovementPressed { get { return isMovementPressed; } }
    public bool IsJumpPressed { get { return isJumpPressed; } }
    public bool IsJumpAnimating { set { isJumpAnimating = value; } }
    public bool IsJumping { get { return isJumping; } set { isJumping = value; } }
    public bool IsRunPressed { get { return isRunPressed; } }
    public int IsJumpingHash { get { return isJumpingHash; } }
    public int IsWalkingHash { get { return isWalkingHash; } }
    public int IsRunningHash { get { return isRunningHash; } }
    public int IsFallingHash { get { return isFallingHash; } }
    public float VelocityY { get { return velocity.y; } set { velocity.y = value; } }

    void Awake()
    {
        inputMaster = new InputMaster();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        // Setup State
        states = new PlayerStateFactory(this);
        currentState = states.Grounded();
        currentState.EnterState();

        // Set Parameter hash references (IDs)
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJumping");
        isFallingHash = Animator.StringToHash("isFalling");

        // Input Callbacks
        inputMaster.Player.Move.started += OnMovementInput;
        inputMaster.Player.Move.canceled += OnMovementInput;
        inputMaster.Player.Move.performed += OnMovementInput;
        inputMaster.Player.Run.started += OnRunInput;
        inputMaster.Player.Run.canceled += OnRunInput;
        inputMaster.Player.Jump.started += OnJumpInput;
        inputMaster.Player.Jump.canceled += OnJumpInput;

        setupJumpVariables();
    }

    void setupJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }

    void Update()
    {
        HandleRotationAndMovement();
        currentState.UpdateStates();

        if (gameObject.transform.position.y < initialJumpPosition - 20)
        {
            Animator.SetBool(isJumpingHash, false);
            Animator.SetBool(isFallingHash, true);
            characterController.enabled = false;
            characterController.transform.position = new Vector3(0, 20, 0);
            characterController.enabled = true;
        }
        if (CharacterController.isGrounded)
            Animator.SetBool(isFallingHash, false);

    }

    // Rotates the player when moving
    void HandleRotationAndMovement()
    {
        if (isMovementPressed)
        {
            // Rotates the player to face the direction of movement
            float targetAngle = Mathf.Atan2(appliedMovement.x, appliedMovement.z) * Mathf.Rad2Deg + mainCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);        
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // Moves the player in the direction of movement
            appliedMovement = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward * appliedMovement.magnitude;
            characterController.Move(appliedMovement * Time.deltaTime);
        }

        // Applies gravity all time
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    void OnJumpInput(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
    }

    void OnRunInput(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    }

    void OnMovementInput(InputAction.CallbackContext ctx)
    {
        currentMovementInput = ctx.ReadValue<Vector2>() * 2f;
        currentMovement.x = currentMovementInput.x;
        currentMovement.z = currentMovementInput.y;
        currentRunMovement.x = currentMovementInput.x * runMultiplier;
        currentRunMovement.z = currentMovementInput.y * runMultiplier;
        isMovementPressed = currentMovementInput.magnitude != 0;
    }

    void OnEnable()
    {
        inputMaster.Enable();
    }

    void OnDisable()
    {
        inputMaster.Disable();
    }
}
