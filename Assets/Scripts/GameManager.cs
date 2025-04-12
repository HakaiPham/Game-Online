using Fusion.Sockets;
using Fusion;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager : NetworkBehaviour, INetworkRunnerCallbacks
{
    public NetworkPrefabRef _player1Prefab;
    public NetworkPrefabRef _player2Prefab;
    public NetworkPrefabRef _player3Prefab;
    public NetworkPrefabRef _player4Prefab;
    [SerializeField] private Transform spawnP1;
    [SerializeField] private Transform spawnP2;
    [SerializeField] private Transform spawnP3;
    [SerializeField] private Transform spawnP4;

    public Transform spawnPoint;
    public NetworkRunner _runner;
    public NetworkSceneManagerDefault _sceneManager;

    void Awake()
    {
        if (_runner == null)
        {
            GameObject runnerObj = new GameObject("NetworkRunner");
            _runner = runnerObj.AddComponent<NetworkRunner>();
            _runner.AddCallbacks(this);
            _sceneManager = runnerObj.AddComponent<NetworkSceneManagerDefault>();
        }

        ConnectToFusion();
    }


    async void ConnectToFusion()
    {
        Debug.Log("Connecting to Fusion Network...");
        _runner.ProvideInput = true;
        string sessionName = "MyGameSession";

        var startGameArgs = new StartGameArgs()
        {
            GameMode = GameMode.Shared,
            SceneManager = _sceneManager,
            SessionName = sessionName,
            PlayerCount = 4,
            IsVisible = true,
            IsOpen = true,
        };

        var result = await _runner.StartGame(startGameArgs);
        if (result.Ok)
        {
            Debug.Log("Connected to Fusion Network successfully!");
        }
        else
        {
            Debug.Log($"Failed to connect: {result.ShutdownReason}");
        }
    }

    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {

    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {

    }

    public int playerIndex = 0;
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log(".....Player Joined: " + player);

        if (_runner.LocalPlayer != player) return;

        string playerClass = PlayerPrefs.GetString("PlayerClass");
        string playerName = PlayerPrefs.GetString("PlayerName");

        NetworkPrefabRef prefab;

        switch (playerClass)
        {
            case "p1":
                prefab = _player1Prefab;
                spawnPoint = spawnP1;
                break;
            case "p2":
                prefab = _player2Prefab;
                spawnPoint = spawnP2;
                break;
            case "p3":
                prefab = _player3Prefab;
                spawnPoint = spawnP3;
                break;
            case "p4":
                prefab = _player4Prefab;
                spawnPoint = spawnP4;
                break;
            default:
                Debug.LogWarning("Unknown player class: " + playerClass);
                prefab = _player1Prefab;
                spawnPoint = spawnP1; // fallback
                break;
        }

        Vector3[] spawnPositions =
        {
            new Vector3(-3, 0, 0),
            new Vector3(-1, 0, 0),
            new Vector3(1, 0, 0),
            new Vector3(3, 0, 0),
        };
        int index = int.Parse(playerClass.Substring(1)) - 1;
        Vector3 position = spawnPositions[index];

        _runner.Spawn
        (
            prefab,
            spawnPoint.position,
            spawnPoint.rotation,
            player,
             (r, o) =>
             {
                Debug.Log("Player spawned: " + o);
             }
        );
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {

    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {

    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {

    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {

        request.Accept();
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {

    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {

    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {

    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {

    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {

    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {

    }

    public void OnConnectedToServer(NetworkRunner runner)
    {

    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {

    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {

    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {

    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {

    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {

    }
}

