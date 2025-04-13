using Fusion;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController: MonoBehaviour
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

    public void FixedUpdate()
    {
        Move();
        Jump();
    }

    void Move()
    {
        moveInput = Input.GetAxis("Horizontal");

        if (moveInput != 0)
        {
            animator.SetBool("Run", true);
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
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
        //Debug.Log("isGrounded: " + isGrounded);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Debug.Log(">>>>>>>");
            animator.SetTrigger("jump");
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
        else
        {
            animator.SetTrigger("Fall");
            if (isGrounded && moveInput == 0)
            {
                animator.SetTrigger("Idle");
            }
        }
    }

    public void EatFruit()
    {

        Debug.Log("Fruit eaten!");

        // TODO: Add any UI update, sound effects, or animations here
    }
}