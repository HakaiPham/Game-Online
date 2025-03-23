using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    public GameObject PlayerPrefab;
    public void PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
        {
            var position = new Vector3(0, 0, 0);
            Runner.Spawn(PlayerPrefab, position, Quaternion.identity);
        }
    }
}
