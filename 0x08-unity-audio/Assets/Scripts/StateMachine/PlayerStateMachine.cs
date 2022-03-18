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

    float turnSmoothVelocity, turnSmoothTime;


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
    public float rotationFactorPerFrame = 15.0f, runMultiplier = 6.0f;
    float gravity = -9.81f, groundedGravity = -9.81f;

    // Jumping Variables
    bool isJumpPressed = false;
    float initialJumpVelocity;
    float initialJumpPosition;
    public float maxJumpHeight = 5f;
    public float maxJumpTime = 0.5f;
    bool isJumping = false;
    bool isJumpAnimating;

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
        gravity = (-2 * maxJumpHeight) / (timeToApex * timeToApex);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        handleRotation();

        if (gameObject.transform.position.y < initialJumpPosition - 5)
        {
            Animator.SetBool(isJumpingHash, false);
            Animator.SetBool(isFallingHash, true);
            characterController.enabled = false;
            characterController.transform.position = new Vector3(0, 20, 0);
            characterController.enabled = true;
        }
        if (CharacterController.isGrounded)
            Animator.SetBool(isFallingHash, false);

        currentState.UpdateStates();

        characterController.Move(appliedMovement * Time.deltaTime);
    }

    void handleRotation()
    {
        Vector3 positionToLookAt;
        // Where Character should point to
        positionToLookAt.x = currentMovement.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = currentMovement.z;
        // Current Rotation of the character
        Quaternion currentRotation = transform.rotation;

        if (isMovementPressed)
        {
            // Calculate the rotation based on when the player is currently pressing
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime);
        }
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
