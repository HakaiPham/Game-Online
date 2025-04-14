using UnityEngine;
using Fusion;

public class DeathZone : NetworkBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!Object.HasStateAuthority) return;

        // Check if the object is a player
        if (other.CompareTag("Player"))
        {
            NetworkObject netObj = other.GetComponent<NetworkObject>();

            if (netObj != null)
            {
                Runner.Despawn(netObj);
            }
        }
    }
}
