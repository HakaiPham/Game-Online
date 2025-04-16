using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Fusion;// Required for scene loading

public class Gate : NetworkBehaviour
{
    public NetworkRunner networkRunner;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Ensure only the player triggers it
        {
            StartCoroutine(TransferPlayer());
            if (networkRunner.IsSceneAuthority)
            {
                // Tải các scene theo chế độ Additive
                networkRunner.LoadScene(SceneRef.FromIndex(0), LoadSceneMode.Additive); // Scene 1
                networkRunner.LoadScene(SceneRef.FromIndex(1), LoadSceneMode.Additive); // Scene 2
                networkRunner.LoadScene(SceneRef.FromIndex(2), LoadSceneMode.Additive); // Scene 3

                Debug.Log("Đang tải 3 scene trong chế độ Additive...");
            }
        }
    }

    private IEnumerator TransferPlayer()
    {
        yield return new WaitForSeconds(2f); // Wait 2 seconds

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1; // Get next scene index
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex); // Load next level
        }
        else
        {
            Debug.Log("No more levels available.");
        }
    }

}
