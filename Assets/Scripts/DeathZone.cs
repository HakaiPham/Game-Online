using UnityEngine;
using Fusion;

public class DeathZone : NetworkBehaviour
{
    // This script handles the death zone collision detection and player death logic.
    private void OnTriggerEnter(Collider other)
    {
        if (!Object.HasStateAuthority) return;

        if (other.CompareTag("Player"))
        {
            PlayerDeath death = other.GetComponent<PlayerDeath>();
            if (death != null)
            {
                death.Die();
            }
        }
    }
}
