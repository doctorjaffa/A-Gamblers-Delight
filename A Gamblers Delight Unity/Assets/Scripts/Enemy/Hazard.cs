using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    // Amount of damage this hazard deals
    //[Header("Positive number for damage the hazard should deal")]
    private int hazardDamage;

    // When this object collides with another object
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collision Detected");

        // Get health component from the object
        Coins coinsScript = collision.gameObject.GetComponent<Coins>();

        // If the object has collided with the player
        if (coinsScript)
        {
            ///Debug.Log("Collided with player!");
            // Deal damage to the player
            coinsScript.ChangeCoins(-HazardDamage());
        }
    }

    private int HazardDamage()
    {
        hazardDamage = Random.Range(1, 70);

        return hazardDamage;
    }
}


