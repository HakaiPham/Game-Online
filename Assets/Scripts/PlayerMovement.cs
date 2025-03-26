using Fusion;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 12f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    float moveInput;

    private Rigidbody2D rb;
    private bool isGrounded;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public override void FixedUpdateNetwork()
    {
        if (!Object.HasStateAuthority) return;
        Move();
        Jump();
    }

    void Move()
    {
         moveInput = Input.GetAxis("Horizontal");

        if (moveInput != 0) {
            animator.SetBool("Run", true);
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        } 
        else if (moveInput == 0)
        {
            animator.SetBool("Run", false);
        }
        // Flip sprite based on movement direction
        if (moveInput > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(transform.position, 1f, groundLayer);
        Debug.Log("isGrounded: " + isGrounded);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Debug.Log(">>>>>>>");
            animator.SetTrigger("jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        else
        {
            animator.SetTrigger("Fall");
            if(isGrounded&&moveInput==0) 
            {
                animator.SetTrigger("Idle");
            }
        }
    }
}