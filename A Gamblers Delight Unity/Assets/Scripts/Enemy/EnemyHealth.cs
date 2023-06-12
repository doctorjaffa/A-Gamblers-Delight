using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //
    // -------------------------------------------------------------------------------------------- VARIABLES INITIALISATION -------------------------------------------------------------------------------------------- //
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //

    // Serialized variables
    // Health the player spawns in with
    [SerializeField]
    private int startingHealth;

    // Private variables
    // Current health of the player
    private int currentHealth;

    // Invincibility frames time
    private float hitInvincibilityMaxTime = 0.4f;
    // How long it has been since the player was hit
    private float lastHitTime = 0;

    // Reference to coins object in scene
    private Coins coins;

    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //
    // ------------------------------------------------------------------------------------------- GETTER/SETTER FUNCTIONS ---------------------------------------------------------------------------------------------- //
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //

    // Getter for currentHealth variable
    public int GetHealth()
    {
        return currentHealth;
    }

    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //
    // ---------------------------------------------------------------------------------------------------  FUNCTIONS --------------------------------------------------------------------------------------------------- //
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //

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
        int increaseAmount = (((int)Random.Range(1.0f, 100000)));
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

    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //
    // ------------------------------------------------------------------------------------------------ END OF FUNCTIONS ------------------------------------------------------------------------------------------------ //
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //
}
