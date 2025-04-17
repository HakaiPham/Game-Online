using UnityEngine;
using Fusion;

public class Trap : NetworkBehaviour
{
    public float LimitUp;
    public float LimitDown;
    public float Speed;
    private bool movingUp = false;

    void Update()
    {
        // This trap doesn’t need StateAuthority if it's just moving locally for all clients
        Vector3 movement = (movingUp ? Vector3.up : Vector3.down) * Speed * Time.deltaTime;
        transform.position += movement;

        if (movingUp && transform.position.y >= LimitUp) movingUp = false;
        else if (!movingUp && transform.position.y <= LimitDown) movingUp = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Object.HasStateAuthority) return;

        if (collision.CompareTag("Player"))
        {
            Debug.Log("Trap triggered: " + collision.name);
            PlayerDeath player = collision.GetComponent<PlayerDeath>();
            if (player != null)
            {
                player.Die();
            }
        }
    }
}
