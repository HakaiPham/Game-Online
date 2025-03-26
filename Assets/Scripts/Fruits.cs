using UnityEngine;
using Fusion;

public class Fruit : NetworkBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player touched the fruit!");

            gameObject.SetActive(false);
        }
    }
}
