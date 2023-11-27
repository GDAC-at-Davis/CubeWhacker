using System;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Handles spawning enemies
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private float spawnInterval = 5f;

    [SerializeField]
    private Vector2 spawnCountRange;

    [SerializeField]
    private Vector2 spawnBounds;

    [SerializeField]
    private int maxEnemyCount;

    public static int EnemyCount;

    private float timer;

    private void Awake()
    {
        EnemyCount = 0;
    }

    private void Update()
    {
        if(EnemyCount >= maxEnemyCount) 
            return;
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            var spawnCount = Random.Range(spawnCountRange.x, spawnCountRange.y);
            for (var i = 0; i < spawnCount; i++)
            {
                var spawnPosition = new Vector3(
                    Random.Range(-spawnBounds.x, spawnBounds.x),
                    10f,
                    Random.Range(-spawnBounds.y, spawnBounds.y)
                );
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            }

            timer = spawnInterval;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnBounds.x * 2f, 1f, spawnBounds.y * 2f));
    }
}