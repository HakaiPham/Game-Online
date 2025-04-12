using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fan : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Animator animator;
    [SerializeField] Vector2 right;
    [SerializeField] Vector2 left;
    Rigidbody2D rb;
    public float speed;
    Vector2 direction;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        direction = Vector2.right;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Move());
    }
    IEnumerator Move()
    {
        if (transform.position.x >= right.x)
        {
            animator.SetBool("FanRun", false);
            yield return new WaitForSeconds(1f);
            direction = Vector2.left;
        }
        else if (transform.position.x <= left.x)
        {
            animator.SetBool("FanRun", false);
            yield return new WaitForSeconds(1f);
            direction = Vector2.right;
        }
        animator.SetBool("FanRun", true);
        rb.linearVelocity = direction * speed;
    }
}
