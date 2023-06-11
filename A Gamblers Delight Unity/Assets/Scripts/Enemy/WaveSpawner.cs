using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField]
    private int waveDuration;
    [SerializeField]
    private List<Enemy> enemies = new List<Enemy>();
    [SerializeField]
    public List<GameObject> bosses = new List<GameObject>();

    private List<GameObject> enemiesToSpawn = new List<GameObject>();

    private int currentWave;
    private int waveValue;
    private float waveTimer;

    private Vector3 spawnPoint;
    private float spawnInterval;
    private float spawnTimer;
    private Vector3 enemySpawnPoint;

    private bool bossWave = false;
    private bool bossSpawned = false;

    [SerializeField]
    private GameObject normalWaveArena;
    [SerializeField]
    private GameObject bossWaveArena;

    // Start is called before the first frame update
    void Start()
    {
        GenerateWave();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // If this frame isn't a boss wave, run the normal wave code
        if (!bossWave)
        {
            // If the spawn timer is at 0, spawn a new enemy
            if (spawnTimer <= 0)
            {
                SetSpawnPosition();

                // Spawn an enemy
                if (enemiesToSpawn.Count > 0)
                {
                    // Spawn first enemy in the list
                    Instantiate(enemiesToSpawn[0], spawnPoint, Quaternion.identity);
                    // Remove the enemy from the list
                    enemiesToSpawn.RemoveAt(0);

                    // Reset the spawn timer
                    spawnTimer = spawnInterval;
                }
            }
            else
            {
                // Decrease the spawn and waver timers
                spawnTimer -= Time.fixedDeltaTime;
                waveTimer -= Time.fixedDeltaTime;
            }
        }

        if (waveTimer <= 0 && !bossSpawned)
        {
            currentWave++;
            GenerateWave();
        }
    }

    public void GenerateWave()
    {
        // If a wave is a boss wave, set the boolean to true
        if (currentWave == 3 || currentWave == 6)
        {
            bossWave = true;
        }

        // If it is not a boss wave, set up and create a new normal wave
        if (!bossWave)
        {
            // Create a wave value - the maximum cost for this wave to be able to "buy" enemies
            waveValue = currentWave * 10;
            // Generate a new list of enemies 
            GenerateEnemies(waveValue);

            // Gives a fixed time between each enemy
            spawnInterval = waveDuration / enemiesToSpawn.Count;

            // Store how long the wave has lasted
            waveTimer = waveDuration;
        }
    }

    public void GenerateEnemies(int waveValue)
    {
        // Create a list of enemies to generate
        List<GameObject> generatedEnemies = new List<GameObject>();

        // Grab a random enemy and check if it can be afforded
        while (waveValue > 0)
        {
            int randEnemyID = Random.Range(0, enemies.Count);
            int randEnemyCost = enemies[randEnemyID].cost;

            // If it can be afforded, add the enemy to the list
            if (waveValue-randEnemyCost >= 0)
            {
                generatedEnemies.Add(enemies[randEnemyID].enemyPrefab);
                waveValue -= randEnemyCost;
            } 
            // If the waveValue has been spent, break the loop
            else if (waveValue <= 0)
            {
                break;
            }
        }

        // Clear the enemiesToSpawn list and populate it with the new generated enemies
        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;
    }

    private void SetSpawnPosition()
    {
        // Set the spawn position to be a random location within the arena 
        spawnPoint.Set(Random.Range(-8.0f, 8.0f), Random.Range(-6.0f, 6.0f), 0);
    }

    private void SpawnBoss()
    {
        bossSpawned = true;
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

// Enemy prefabs class for this script to access
[System.Serializable]
public class Enemy
{
    public GameObject enemyPrefab;
    public int cost;
}

// Boss prefabs class for this script to access
[System.Serializable]
public class Boss
{
    public GameObject bossPrefab;
}
