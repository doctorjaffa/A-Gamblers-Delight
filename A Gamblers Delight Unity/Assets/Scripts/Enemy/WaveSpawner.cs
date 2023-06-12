using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //
    // -------------------------------------------------------------------------------------------- VARIABLES INITIALISATION -------------------------------------------------------------------------------------------- //
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //

    // Serialized fields
    // Enemies list (for prefabs)
    [SerializeField]
    private List<Enemy> enemies = new List<Enemy>();
    // Bosses list (for prefabs)
    [SerializeField]
    public List<GameObject> bosses = new List<GameObject>();

    // Countdown for the current wave
    [SerializeField]
    private float waveTimer;
    // How long a wave lasts for
    [SerializeField]
    private int waveDuration = 20;

    // Private variables
    // The enemies to spawn in a current wave
    private List<GameObject> enemiesToSpawn = new List<GameObject>();

    // The current wave and its total cost
    private int currentWave = 1;
    private int waveValue;

    // The spawn variables for an enemy 
    private Vector3 spawnPoint;
    private float spawnInterval = 3;
    private float spawnTimer = 3;
    private Vector3 enemySpawnPoint;

    // Booleans to mark if it is a boss wave, and if a boss has already spawned
    private bool bossWave = false;
    private bool bossSpawned = false;

    // Boolean to mark if a wave is currently active
    private bool waveInProgress = false;

    // The normal and boss wave arenas
    [SerializeField]
    private GameObject normalWaveArena;
    [SerializeField]
    private GameObject bossWaveArena;

    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //
    // ----------------------------------------------------------------------------------------  END OF VARIABLES INITIALISATION ---------------------------------------------------------------------------------------- //
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //

    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //
    // ------------------------------------------------------------------------------------------- GETTER/SETTER FUNCTIONS ---------------------------------------------------------------------------------------------- //
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //

    // Setter to be called upon a boss dying 
    public void SetBossSpawnedToFalse()
    {
        bossSpawned = false;
    }

    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //
    // ---------------------------------------------------------------------------------------------------  FUNCTIONS --------------------------------------------------------------------------------------------------- //
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //

    // Awake is called on object creation
    void Awake()
    {
        // Set the wave timer to equal the duration of a wave
        waveTimer = waveDuration;
        // Create the first wave - The current wave is already set to 1
        GenerateWave();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // If there is not a wave in progress, enter this block of code to create a wave
        if (!waveInProgress)
        {
            // Create a new populated wave
            GenerateWave();
            // A wave is now in progress
            waveInProgress = true;
        }

        // There is a wave in progress, so run the wave code
        if (waveInProgress)
        {
            // If this is not a boss wave, run the normal wave code
            if (!bossWave)
            {
                // Run this code until the wave timer runs out
                do
                {
                    // If the spawn timer is at 0, spawn a new enemy
                    if (spawnTimer <= 0)
                    {
                        // Find a random spawn position
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

                    // Decrease the spawn and waver timers
                    spawnTimer -= Time.fixedDeltaTime;
                    waveTimer -= Time.fixedDeltaTime;

                } while (waveTimer > 0); // Until the wave timer runs out
            }
            // If it is a boss wave, run the boss wave code
            else if (bossWave)
            {
                // Ensure a new wave does not start until the boss is killed
                do
                {
                    // Do nothing until the boss dies
                } while (bossSpawned);
            }
        }

        // This happens after we break out of the wave loop - when a wave is over
        // Set the waveInProgress to false - allowing a new wave to be created in the next frame
        waveInProgress = false;
        // Increment the current wave 
        currentWave++;
    }

    // Creates a new wave
    private void GenerateWave()
    {
        // If a wave is a boss wave, set the boolean to true
        if (currentWave == 5 || currentWave == 10)
        {
            bossWave = true;
        }
        // Otherwise, set it to false
        else
        {
            bossWave = false;
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
        }
        // If it is a boss wave, spawn the boss
        else if (bossWave)
        {
            SpawnBoss();
        }
    }

    // Creates a list of enemies to be spawned
    private void GenerateEnemies(int waveValue)
    {
        // Create a list of enemies to generate
        List<GameObject> generatedEnemies = new List<GameObject>();

        // Grab a random enemy and check if it can be afforded
        while (waveValue > 0)
        {
            int randEnemyID = Random.Range(0, enemies.Count);
            int randEnemyCost = enemies[randEnemyID].cost;

            // If it can be afforded, add the enemy to the list
            if (waveValue - randEnemyCost >= 0)
            {
                generatedEnemies.Add(enemies[randEnemyID].enemyPrefab);
                // Remove the enemy cost from the wave's total value
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

    // Sets a new random spawn position (for enemies)
    private void SetSpawnPosition()
    {
        // Set the spawn position to be a random location within the arena 
        spawnPoint.Set(Random.Range(-8.0f, 8.0f), Random.Range(-6.0f, 6.0f), 0);
    }

    // Spawns a boss
    private void SpawnBoss()
    {
        // Set bossSpawned to true
        bossSpawned = true;
        // Swap to the boss arena
        SwitchRoom();


        int randBossID = Random.Range(0, bosses.Count);

        Instantiate(bosses[randBossID]);
    }

    // Changes between the normal and boss arenas
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

    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //
    // ------------------------------------------------------------------------------------------------ END OF FUNCTIONS ------------------------------------------------------------------------------------------------ //
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //
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
