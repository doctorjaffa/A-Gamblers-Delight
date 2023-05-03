using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Add the UI library to this script
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    // Contains slider component attached to this object
    private Slider healthBar;

    // PlayerHealth component for information on a player's health
    PlayerHealth player;

    EnemyHealth enemy;

    // Start is called before the first frame update
    void Start()
    {
        // Store the slider component in the healthBar variable
        healthBar = GetComponent<Slider>();

        // Find the player in the scene by its PlayerHealth script 
        player = FindObjectOfType<PlayerHealth>();   
    }

    // Update is called once per frame
    void Update()
    {

        // Create temporary float variables to use float division
        float currentPlayerHealth = player.GetHealth();
        float maxPlayerHealth = player.startingHealth;

        // Update the health bar to match the current health
        healthBar.value = currentPlayerHealth / maxPlayerHealth;
    }
}
