using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : ObjectProjectile
{
    //Private variables
    // Objects in the scene
    private GameObject target;

    // When this object collides with another object
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        //Debug.Log("Collision Detected");

        // Get health component from the object
        PlayerGamble playerGambleScript = FindObjectOfType<PlayerGamble>();

        // If the object has collided with the player
        if (playerGambleScript)
        {
            ///Debug.Log("Collided with player!");
            // Deal damage to the player
            playerGambleScript.GambleCoins();

        }
    }
}
