using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    // Start is called before the first frame update
    public float LimitUp;
    public float LimitDown;
    public float Speed;
    Rigidbody2D Rigidbody;
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (transform.position.y >= LimitUp)
        {
            Rigidbody.velocity = Vector2.down * Speed ;
        }
        else if (transform.position.y <= LimitDown)
        {
            Rigidbody.velocity = Vector2.up * Speed;
        }
    }
}
