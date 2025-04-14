using UnityEngine;
using Fusion;
public enum FruitType { Banana, Apple, Melon, Cherries, Kiwi }

public class Fruit : NetworkBehaviour
{
    public FruitType fruitType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (ScoreManager.Instance == null)
            {
                Debug.LogError("ScoreManager is missing in the scene!");
                return;
            }

            ScoreManager.Instance.AddScore(fruitType);
            Runner.Despawn(Object); // nếu muốn destroy luôn
        }
    }
}
