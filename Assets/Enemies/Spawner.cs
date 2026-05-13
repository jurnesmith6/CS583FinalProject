using System.Collections;
using System.Data;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public static Spawner instance { get; private set; }
    public Enemy[] enemies;
    public float spawnRadius;
    public float forestEdgeDistance;
    public float spawnInterval;
    public float firstWaveDelay;
    public int wave { get; private set; } = 1;
    public float timeBetweenWaves;
    public string enemyFormula;
    public int enemiesRemaining;

    Vector3 center = Vector3.zero;
    Vector3 attackDirection;
    Vector3 spawnLocation;
    DataTable dataTable = new();

    void Start() {
        instance = this;
        Invoke("StartWave", firstWaveDelay);
    }

    // start spawning enemies in the forest from a random direction
    public void StartWave() {
        enemiesRemaining = System.Convert.ToInt32(dataTable.Compute(enemyFormula.Replace("x", (wave - 1).ToString()), null));
        attackDirection = Random.insideUnitCircle.normalized;
        spawnLocation = center + new Vector3(attackDirection.x, 0f, attackDirection.y) * (forestEdgeDistance + spawnRadius);

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
            Invoke("StartWave", timeBetweenWaves);
        }
    }
}
