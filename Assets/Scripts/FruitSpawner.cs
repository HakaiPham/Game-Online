using UnityEngine;
using Fusion;

public class FruitSpawner : NetworkBehaviour
{
    public NetworkPrefabRef[] fruitPrefabs; // Gán trong Inspector: Apple, Banana, etc.
    public Transform[] spawnPoints;         // Gán trong Inspector: các vị trí spawn

    public override void Spawned()
    {
        if (Object.HasStateAuthority)
        {
            SpawnRandomFruit();
        }
    }

    void SpawnRandomFruit()
    {
        if (fruitPrefabs.Length == 0 || spawnPoints.Length == 0)
        {
            Debug.LogWarning("FruitSpawner: Không có prefab hoặc spawn point nào!");
            return;
        }

        // Chọn ngẫu nhiên prefab và vị trí
        NetworkPrefabRef randomFruit = fruitPrefabs[Random.Range(0, fruitPrefabs.Length)];
        Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        Runner.Spawn(randomFruit, randomPoint.position, Quaternion.identity);
    }
}
