using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Public variables

    // Health the player spawns in with
    public int startingHealth = 5;

    // Current health of the player
    private int currentHealth = 5;

    // Invincibility frames time
    public float hitInvincibilityMaxTime = 0.4f;

    // How long it has been since the player was hit
    private float lastHitTime = 0;



    void Awake()
    {
        // Set the current health to be equal to the starting health on spawn
        currentHealth = startingHealth;
    }

    // Kill the enemy object
    public void Kill()
    {
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
