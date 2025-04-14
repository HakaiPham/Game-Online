using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapFire : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Animator animator;
    public bool isFireOn = false;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            StartCoroutine(WaitSecond());
            if(isFireOn)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(collision.rigidbody.linearVelocity.x, 12f+4f);
                collision.gameObject.GetComponent<CapsuleCollider2D>().isTrigger = true;
            }
        }
    }
    IEnumerator WaitSecond()
    {
        animator.SetTrigger("Hit");
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("On");
        isFireOn = true;
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("Off");
        isFireOn = false;
    }
}
