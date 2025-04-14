using Fusion.Sockets;
using Fusion;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager : NetworkBehaviour, INetworkRunnerCallbacks
{
    [Header("Player Prefabs")]
    public NetworkPrefabRef _player1Prefab;
    public NetworkPrefabRef _player2Prefab;
    public NetworkPrefabRef _player3Prefab;
    public NetworkPrefabRef _player4Prefab;

    [Header("Player Spawn Points")]
    [SerializeField] private Transform spawnP1;
    [SerializeField] private Transform spawnP2;
    [SerializeField] private Transform spawnP3;
    [SerializeField] private Transform spawnP4;

    [Header("Fruit Spawn Setup")]
    public NetworkPrefabRef[] fruitPrefabs;      // Add prefab references in Inspector
    public Transform[] fruitSpawnPoints;         // Add positions in Inspector

    private bool fruitSpawned = false;

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

        var startGameArgs = new StartGameArgs()
        {
            GameMode = GameMode.Shared,
            SceneManager = _sceneManager,
            SessionName = "MyGameSession",
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
        SpawnScoreManager();
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
                spawnPoint = spawnP1;
                break;
        }

        _runner.Spawn(prefab, spawnPoint.position, spawnPoint.rotation, player, (runner, obj) =>
        {
            Debug.Log("Player spawned: " + obj);
        });

        // 🔸 Spawn fruit only once when host joins
        if (!fruitSpawned && player == _runner.LocalPlayer)
        {
            fruitSpawned = true;
            SpawnRandomFruit();
        }
    }
    public NetworkPrefabRef scoreManagerPrefab;
    private NetworkObject scoreManagerInstance;

    public void SpawnScoreManager()
    {
        scoreManagerInstance = _runner.Spawn(scoreManagerPrefab, Vector3.zero, Quaternion.identity);
    }
    void SpawnRandomFruit()
    {
        if (fruitPrefabs.Length == 0 || fruitSpawnPoints.Length == 0)
        {
            Debug.LogWarning("Fruit prefabs hoặc spawn points chưa gán!");
            return;
        }

        foreach (Transform spawn in fruitSpawnPoints)
        {
            NetworkPrefabRef randomFruit = fruitPrefabs[UnityEngine.Random.Range(0, fruitPrefabs.Length)];

            _runner.Spawn(randomFruit, spawn.position, Quaternion.identity, null, (runner, obj) =>
            {
                obj.transform.localScale = Vector3.one;
            });
        }

        Debug.Log("Spawned random fruits at all spawn points with scale = 1!");
    }

    // Fusion callback stubs
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) { }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) => request.Accept();
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) { }
    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }
    public void OnInput(NetworkRunner runner, NetworkInput input) { }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnConnectedToServer(NetworkRunner runner) { }
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
    public void OnSceneLoadDone(NetworkRunner runner) { }
    public void OnSceneLoadStart(NetworkRunner runner) { }
}
