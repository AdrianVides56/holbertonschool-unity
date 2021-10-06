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

    // Update is called once per frame
    void Update()
    {
        if (inputMaster.Player.Move.ReadValue<Vector2>().magnitude >= 0.1f)
            animator.SetBool("isWalking", true);
        else
            animator.SetBool("isWalking", false);

        if (inputMaster.Player.Jump.triggered)
                animator.SetBool("isJumping", true);
        else
            animator.SetBool("isJumping", false);
        
        if (player.transform.position.y < -15f && !player.GetComponent<PlayerController>().isGrounded)
            animator.SetBool("isFalling", true);
        else if (player.GetComponent<PlayerController>().isGrounded)
            animator.SetBool("isFalling", false);
        
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
