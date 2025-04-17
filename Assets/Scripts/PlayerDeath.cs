using UnityEngine;
using Fusion;

public class PlayerDeath : NetworkBehaviour
{
    //Chống trôi
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
