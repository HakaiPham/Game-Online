using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public float LimitUp;
    public float LimitDown;
    public float Speed = 10f;
    private bool movingUp = false;

    void Update()
    {

        if (movingUp)
        {
            transform.position += Vector3.up * Speed * Time.deltaTime;
            if (transform.position.y >= LimitUp)
            {
                movingUp = false;
            }
        }
        else
        {
            transform.position += Vector3.down * Speed * Time.deltaTime;
            if (transform.position.y <= LimitDown)
            {
                movingUp = true;
            }
        }
    }

}
