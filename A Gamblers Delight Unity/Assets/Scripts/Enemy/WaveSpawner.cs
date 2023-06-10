using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{

    public List<Enemy> enemies = new List<Enemy>();
    public int currentWave;
    public int waveValue;

    public List<GameObject> enemiesToSpawn = new List<GameObject>();

    public Transform spawnPoint;
    public int waveDuration;
    private float waveTimer;
    private float spawnInterval;
    private float spawnTimer;

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
            // Spawn an enemy.
            if (enemiesToSpawn.Count > 0)
            {
                // Spawn first enemy in the list. 
                Instantiate(enemiesToSpawn[0], spawnPoint.position, Quaternion.identity);
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
        else
        {
            spawnTimer -= Time.fixedDeltaTime;
            waveTimer -= Time.fixedDeltaTime;
        }
    }

    public void GenerateWave()
    {
        waveValue = currentWave * 10;
        GenerateEnemies();

        // Gives a fixed time between each enemy.
        spawnInterval = waveDuration / enemiesToSpawn.Count;
        
        // Store how long the wave has lasted.
        waveTimer = waveDuration;
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
}

[System.Serializable]
public class Enemy
{
    public GameObject enemyPrefab;
    public int cost;
}
