using Fusion;
using UnityEngine;

public class NetworkInputHandler : MonoBehaviour
{
    private NetworkRunner _runner;

    private void Start()
    {
        _runner = FindObjectOfType<NetworkRunner>(); // Tìm NetworkRunner trong scene
    }

    private void Update()
    {
        if (_runner == null || !_runner.IsClient) return;
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        NetworkInputData inputData = new NetworkInputData
        {
            movement = new Vector2(Input.GetAxisRaw("Horizontal"), 0),
            jumpPressed = Input.GetKeyDown(KeyCode.Space)
        };

        input.Set(inputData);
    }
}