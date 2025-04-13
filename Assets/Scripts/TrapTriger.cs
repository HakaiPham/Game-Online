using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTriger : MonoBehaviour
{
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
