using Fusion;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerMovement : NetworkBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 12f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    float moveInput;

    private Rigidbody2D rb;
    private CapsuleCollider2D collider;
    private bool isGrounded;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<CapsuleCollider2D>();
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "NemNhay")
        {
            animator.SetTrigger("jump");
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce + 6f);
        }
        if (collision.gameObject.tag == "Wall"&& collision.gameObject.tag == "Blink")
        {
            collider.enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Arrow")
        {
            animator.SetTrigger("jump");
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce + 7f);
        }
        if (collision.gameObject.tag == "Gai")
        {
            collider.enabled = false;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce + 7f);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "NemNhay")
        {
            animator.SetTrigger("Fall");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Arrow")
        {
            animator.SetTrigger("Fall");
        }
    }
    public void EatFruit()
    {
        
        Debug.Log("Fruit eaten!");

        // TODO: Add any UI update, sound effects, or animations here
    }
}