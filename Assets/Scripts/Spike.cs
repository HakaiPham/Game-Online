using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Spike hit: " + collision.gameObject.name);

        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player detected! Calling Die()");
            PlayerDeath player = collision.GetComponentInParent<PlayerDeath>();
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
