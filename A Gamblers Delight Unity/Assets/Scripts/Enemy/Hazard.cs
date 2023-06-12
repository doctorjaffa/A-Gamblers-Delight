using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{

    [SerializeField]
    private float animationDuration;

    // When this object collides with another object
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collision Detected");

        // Get health component from the object
        PlayerGamble playerGambleScript = FindObjectOfType<PlayerGamble>();

        // Get coin component
        Coin coinScript = FindObjectOfType<Coin>();

        // If the object has collided with the player
        if (playerGambleScript && collision.gameObject.layer == 7)
        {
            ///Debug.Log("Collided with player!");
            // Deal damage to the player
            playerGambleScript.GambleCoins();
            
        }
    }
}


