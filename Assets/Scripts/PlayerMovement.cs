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

    public Rigidbody2D rb;
    private bool isGrounded;

    public Animator animator;

    public AudioSource audioSource;
    public AudioClip jumpSound;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator not found on " + gameObject.name);
        }

        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D not found on " + gameObject.name);
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource not found on " + gameObject.name);
        }

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
            RPC_PlayJumpSound();

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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Arrow")
        {
            animator.SetTrigger("jump");
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce +8f);
        }
    }
    public void EatFruit()
    {
        
        Debug.Log("Fruit eaten!");

        // TODO: Add any UI update, sound effects, or animations here
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    void RPC_PlayJumpSound()
    {
        if (jumpSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(jumpSound);
        }
    }

}