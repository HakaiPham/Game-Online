using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; // Needed for scene reloading

public class PlayerDeath : MonoBehaviour
{
    public void Die()
    {
        Debug.Log("Destroying player...");
        Destroy(gameObject);
        StartCoroutine(RespawnPlayer());
    }

    private IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(1f); // Wait 1 second before reloading
        Debug.Log("Reloading scene...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reloads the current scene
    }
}
