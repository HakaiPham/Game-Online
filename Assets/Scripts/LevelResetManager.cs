using UnityEngine;
using Fusion;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelResetManager : NetworkBehaviour
{
    public static LevelResetManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RpcResetLevel()
    {
        Debug.Log("[LevelResetManager] RPC received — resetting level...");
        StartCoroutine(ResetSceneCoroutine());
    }

    public void RequestLevelReset()
    {
        if (HasStateAuthority)
        {
            RpcResetLevel(); // Host triggers the actual reset
        }
    }

    private IEnumerator ResetSceneCoroutine()
    {
        yield return new WaitForSeconds(1.5f); // Short delay so logs/despawn process completes

        if (Runner != null)
        {
            Runner.Shutdown(true, ShutdownReason.Ok); // End current session
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reloads current level
    }
}
