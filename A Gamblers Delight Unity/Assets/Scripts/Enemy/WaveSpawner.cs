using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{

    public List<Enemy> enemies = new List<Enemy>();
    public int currentWave;
    public int waveValue;

    public List<GameObject> enemiesToSpawn = new List<GameObject>();
    public List<GameObject> bosses = new List<GameObject>();

    private Vector3 spawnPoint;
    public int waveDuration;
    private float waveTimer;
    private float spawnInterval;
    private float spawnTimer;
    private Vector3 enemySpawnPoint;

    private bool bossWave = false;
    private bool bossSpawned = false;

    public GameObject normalWaveArena;
    public GameObject bossWaveArena;

    // Start is called before the first frame update
    void Start()
    {
        GenerateWave();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (spawnTimer <= 0)
        {
            SetSpawnPosition();

            // Spawn an enemy.
            if (enemiesToSpawn.Count > 0)
            {
                // Spawn first enemy in the list. 
                Instantiate(enemiesToSpawn[0], spawnPoint, Quaternion.identity);
                // Remove the enemy from the list. 
                enemiesToSpawn.RemoveAt(0);

                // Reset the spawn timer. 
                spawnTimer = spawnInterval;
            }
            else
            {
                waveTimer = 0;
            }
        }
        else if (bossWave)
        {
            SpawnBoss();
            bossSpawned = true;
        }
        else
        {
            spawnTimer -= Time.fixedDeltaTime;
            waveTimer -= Time.fixedDeltaTime;
        }

        if (waveTimer <= 0 && !bossSpawned)
        {
            currentWave++;
            GenerateWave();
        }
    }

    public void GenerateWave()
    {
        if (currentWave == 3 || currentWave == 6)
        {
            bossWave = true;
        }

        if (!bossWave)
        {
            waveValue = currentWave * 10;
            GenerateEnemies();

            // Gives a fixed time between each enemy.
            spawnInterval = waveDuration / enemiesToSpawn.Count;

            // Store how long the wave has lasted.
            waveTimer = waveDuration;
        }
    }

    public void GenerateEnemies()
    {
        // Create a list of enemies to generate.
        List<GameObject> generatedEnemies = new List<GameObject>();

        // Grab a random enemy and check if it can be afforded.
        while (waveValue > 0)
        {
            int randEnemyID = Random.Range(0, enemies.Count);
            int randEnemyCost = enemies[randEnemyID].cost;

            if (waveValue-randEnemyCost >= 0)
            {
                generatedEnemies.Add(enemies[randEnemyID].enemyPrefab);
                waveValue -= randEnemyCost;
            } 
            else if (waveValue <= 0)
            {
                break;
            }
        }

        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;
    }

    private void SetSpawnPosition()
    {
        spawnPoint.Set(Random.Range(-8.0f, 8.0f), Random.Range(-6.0f, 6.0f), 0);
    }

    private void SpawnBoss()
    {
        bossSpawned = false;
        SwitchRoom();


        int randBossID = Random.Range(0, bosses.Count);

        Instantiate(bosses[randBossID]);
    }

    private void SwitchRoom()
    {
        if (normalWaveArena.activeInHierarchy)
        {
            bossWaveArena.SetActive(true);
            normalWaveArena.SetActive(false);
        }
        else
        {
            normalWaveArena.SetActive(true);
            bossWaveArena.SetActive(false);
        }
    }
}

[System.Serializable]
public class Enemy
{
    public GameObject enemyPrefab;
    public int cost;
}

[System.Serializable]
public class Boss
{
    public GameObject bossPrefab;
}
