using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class NewPlayerController : MonoBehaviour
{
    InputMaster inputMaster;
    CharacterController characterController;
    Animator animator;

    // variable to store optimized setter/getter parameter IDs
    int isWalkingHash;
    int isRunningHash;
    int isJumpingHash;

    // Input Values
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;
    Vector3 appliedMovement;
    bool isMovementPressed;
    bool isRunPressed;

    // Constants
    public float rotationFactorPerFrame = 15.0f, runMultiplier = 6.0f;
    float gravity = -9.81f, groundedGravity = -0.05f;

    // Jumping Variables
    bool isJumpPressed = false;
    float initialJumpVelocity;
    public float maxJumpHeight = 1.0f;
    public float maxJumpTime = 0.5f;
    bool isJumping = false;
    bool isJumpAnimating;


    void Awake()
    {
        inputMaster = new InputMaster();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        // Set Parameter hash references (IDs)
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJumping");

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
    void setupJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / (timeToApex * timeToApex);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }

    void handleJump()
    {
         if (!isJumping && characterController.isGrounded && isJumpPressed)
         {
             animator.SetBool(isJumpingHash, true);
             isJumpAnimating = true;
             isJumping = true;
             currentMovement.y = initialJumpVelocity;
             appliedMovement.y = initialJumpVelocity;
         }
         else if (!isJumpPressed && isJumping && characterController.isGrounded)
         {
             isJumping = false;
         }
    }

    void handleAnimation()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);

        // Walking Animation
        if (isMovementPressed && !isWalking) 
            animator.SetBool(isWalkingHash, true);
        else if (!isMovementPressed && isWalking)
            animator.SetBool(isWalkingHash, false);

        // Running Animation
        if ((isMovementPressed && isRunPressed) && !isRunning)
            animator.SetBool(isRunningHash, true);
        else if ((!isMovementPressed || !isRunPressed) && isRunning)
            animator.SetBool(isRunningHash, false);
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

    void handleGravity()
    {
        bool isFalling = currentMovement.y <= 0f || !isJumpPressed;
        float fallMultiplier = 1.25f;
        if (characterController.isGrounded)
        {
            if (isJumpAnimating)
            {
                animator.SetBool(isJumpingHash, false);
                isJumpAnimating = false;
            }
            currentMovement.y = groundedGravity;
            appliedMovement.y = groundedGravity;
        }
        else if (isFalling)
        {
            float previousYVelocity = currentMovement.y;
            currentMovement.y = currentMovement.y + (gravity * fallMultiplier * Time.deltaTime);
            appliedMovement.y = (previousYVelocity + currentMovement.y) * 0.5f;
        }
        else
        {
            float previousYVelocity = currentMovement.y;
            currentMovement.y = currentMovement.y + (gravity * Time.deltaTime);
            appliedMovement.y = (previousYVelocity + currentMovement.y) * 0.5f;
         }
    }

    void Update()
    {
        handleRotation();
        handleAnimation();
        
        if (isRunPressed)
        {
            appliedMovement.x = currentRunMovement.x;
            appliedMovement.z = currentRunMovement.z;
        }
        else
        {
            appliedMovement.x = currentMovement.x;
            appliedMovement.z = currentMovement.z;
        }

        characterController.Move(appliedMovement * Time.deltaTime);

        handleGravity();
        handleJump();
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