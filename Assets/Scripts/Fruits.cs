using UnityEngine;
using Fusion;

public enum FruitType { Banana, Apple, Melon, Cherries, Kiwi }

public class Fruit : NetworkBehaviour
{
    public FruitType fruitType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Object.HasStateAuthority) return;

        if (collision.CompareTag("Player"))
        {
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.RpcAddScore(fruitType);
            }
            else
            {
                Debug.LogWarning("ScoreManager instance not found!");
            }

            Runner.Despawn(Object); // Xoá trái cây sau khi ăn
        }
    }
}
