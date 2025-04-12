using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NemNhun : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            animator.SetBool("NemNhunStart", true);
            StartCoroutine(WaitAnimationStart());
        }
    }
    IEnumerator WaitAnimationStart()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("NemNhunStart", false);
    }
}
