using UnityEngine;
using Fusion;

public class PlayerDeath : NetworkBehaviour
{
    // This script handles the player death logic and level reset request.
    public void Die()
    {
        if (!HasStateAuthority) return;

        Debug.Log("Player died — requesting level reset...");

        if (LevelResetManager.Instance != null)
        {
            LevelResetManager.Instance.RequestLevelReset();
        }
        else
        {
            Debug.LogError("LevelResetManager not found!");
        }

        Runner.Despawn(Object);
    }
}
