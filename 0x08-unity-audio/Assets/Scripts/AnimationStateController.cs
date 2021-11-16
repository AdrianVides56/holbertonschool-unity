using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    private InputMaster inputMaster;
    public GameObject player;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (inputMaster.Player.Move.ReadValue<Vector2>().magnitude >= 0.1f)
            animator.SetBool("isWalking", true);
        else
            animator.SetBool("isWalking", false);

        if (player.GetComponent<PlayerController>().isGrounded)
        {
            if (inputMaster.Player.Jump.triggered)
                animator.SetBool("isJumping", true);
            else
                animator.SetBool("isJumping", false);
        }
        else
        {
            if (player.transform.position.y < -15f)
                animator.SetBool("isFalling", true);
            else
                animator.SetBool("isFalling", false);
        }
        
    }

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
}
