using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Public variables
    // Health the player spawns in with
    public int startingHealth;

    // Current health of the player
    public int currentHealth;

    // Invincibility frames time
    public float hitInvincibilityMaxTime = 0.4f;

    // Private variables
    // How long it has been since the player was hit
    private float lastHitTime = 0;

    // Reference to coins object in scene
    private Coins coins;

    void Awake()
    {
        // Set the current health to be equal to the starting health on spawn
        currentHealth = startingHealth;

        // Find the object which contains Coins in the scene
        coins = FindObjectOfType<Coins>();
    }

    // Kill the enemy object
    public void Kill()
    {
        // Increase the amount of coins owned by player by a random amount
        int increaseAmount = (((int)Random.Range(1.0f, 1000)));
        coins.ChangeCoins(increaseAmount);

        // Destroy the object
        Destroy(gameObject);
    }

    // Change current health by set amount and record when the enemy was hit
    public void ChangeHealth(int changeAmount)
    {
        // If enough time has passed since the enemy was last hit
        if (Time.time >= lastHitTime + hitInvincibilityMaxTime)
        {
            currentHealth += changeAmount;

            // Clamp health between 0 and starting health to avoid negative health or going above maximum health
            currentHealth = Mathf.Clamp(currentHealth, 0, startingHealth);

            // If the player health is equal to or less than 0
            if (currentHealth <= 0)
            {
                // Kill the player 
                Kill();
            }

            // Update the time since the enemy was last hit
            lastHitTime = Time.time;
        }
    }

    // Getter for currentHealth variable
    public int GetHealth()
    {
        return currentHealth;
    }
}
