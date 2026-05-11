using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public static Spawner instance { get; private set; }
    public Enemy[] enemies;
    public float spawnRadius;
    public float forstEdgeDistance;
    public float spawnInterval;
    public int wave { get; private set; } = 1;

    Vector3 center = Vector3.zero;
    Vector3 attackDirection;
    Vector3 spawnLocation;
    int enemiesRemaining;

    void Start() {
        instance = this;
        Invoke("StartWave", 5f);
    }

    // start spawning enemies in the forest from a random direction
    public void StartWave() {
        enemiesRemaining = 10 * wave;
        attackDirection = Random.insideUnitCircle.normalized;
        spawnLocation = center + new Vector3(attackDirection.x, 0f, attackDirection.y) * (forstEdgeDistance + spawnRadius);

        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies() {
        int enemiesToSpawn = enemiesRemaining;

        for (int i = 0; i < enemiesToSpawn; ++i) {
            Vector2 offset = Random.insideUnitCircle * spawnRadius;

            Instantiate(
                enemies[Random.Range(0, enemies.Length)],
                spawnLocation + new Vector3(offset.x, 0f, offset.y),
                Quaternion.identity
            );

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void EnemyKilled() {
        if (--enemiesRemaining <= 0) {
            ++wave;
            Invoke("StartWave", 5f);
        }
    }
}
