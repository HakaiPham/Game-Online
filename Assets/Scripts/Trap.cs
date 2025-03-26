using UnityEngine;
using UnityEngine.SceneManagement;

public class Trap : MonoBehaviour
{
    public float LimitUp;
    public float LimitDown;
    public float Speed;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trap hit: " + collision.gameObject.name); // Debug message

        if (collision.CompareTag("Player")) // Use tag or name
        {
            Debug.Log("Player detected! Calling Die()");
            PlayerDeath player = collision.GetComponent<PlayerDeath>();
            if (player != null)
            {
                player.Die();
            }
            else
            {
                Debug.LogError("PlayerDeath script NOT found on Player!");
            }
        }
    }
}
