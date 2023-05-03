using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Add the scene management library to this script
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    // Public variables

    // Health the player spawns in with
    public int startingHealth = 100;

    // Current health of the player
    private int currentHealth = 100;

    // Invincibility frames time
    public float hitInvincibilityMaxTime = 1;

    // How long it has been since the player was hit
    private float lastHitTime = 0;


    
    void Awake()
    {
        // Set the current health to be equal to the starting health on spawn
        currentHealth = startingHealth;
    }

    // Restart the scene
    public void Kill()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Change current health by set amount and record when the player was hit
    public void ChangeHealth(int changeAmount)
    {
        // If enough time has passed since the player was last hit
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

            // Update the time since the player was last hit
            lastHitTime = Time.time;
        }
    }

    // Getter for currentHealth variable
    public int GetHealth()
    {
        return currentHealth;
    }
}
