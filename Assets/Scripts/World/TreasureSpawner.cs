using UnityEngine;

public class TreasureSpawner : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private Transform spawnPointsParent;
    [SerializeField] private float spawnInterval = 20f;
    [SerializeField] private int amountPerSpawn = 4;

    private PlayerStats stats;
    private float timer;

    private void Start()
    {
        stats = FindAnyObjectByType<PlayerStats>();
    }

    private void Update()
    {
        if (stats.Treasure <= 0) return;
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0;
            SpawnCoin();
        }
    }

    private void SpawnCoin()
    {
        if (spawnPointsParent == null || spawnPointsParent.childCount == 0) return;
        stats.ConsumeTreasure();
        for (var i = 0; i < amountPerSpawn; i++)
        {
            var point = spawnPointsParent.GetChild(Random.Range(0, spawnPointsParent.childCount));
            Instantiate(coinPrefab, point.position, Quaternion.identity);
        }
    }
}
