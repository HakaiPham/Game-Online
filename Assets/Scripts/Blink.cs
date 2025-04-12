using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Vector2 right;
    public Vector2 left;
    public float speed = 3f;
    Animator animator;
    Rigidbody2D rb;
    Vector2 direction;
    public Vector2 huong;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        direction = huong;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Move());
    }
    IEnumerator Move()
    {
        if(transform.position.x >= right.x)
        {
            animator.SetBool("TopHit", true);
            yield return new WaitForSeconds(0.5f);
            animator.SetBool("TopHit", false);
            direction = Vector2.left;
        }
        else if (transform.position.x <= left.x)
        {
            animator.SetBool("BottomHit", true);
            yield return new WaitForSeconds(0.5f);
            animator.SetBool("BottomHit", false);
            direction = Vector2.right;
        }
        rb.linearVelocity = direction * speed;
    }
}
