using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    // Amount of damage this hazard deals
    [Header("Positive number for damage the hazard should deal")]
    public int hazardDamage;

    // When this object collides with another object
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision Detected");

        // Get health component from the object
        PlayerHealth healthScript = collision.gameObject.GetComponent<PlayerHealth>();

        // If the object has collided with the player
        if (healthScript)
        {
            Debug.Log("Collided with player!");
            // Deal damage to the player
            healthScript.ChangeHealth(-hazardDamage);
        }
    }

}
