using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // PowerUpEffect object
    [SerializeField]
    private PowerUpEffect powerUpEffect;

    private GameObject target;

    // When this object comes into contact with another object
    private void OnTriggerEnter2D(Collider2D collision)
    {

        // Check for player
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            // Destroy the power up and apply an effect
            Destroy(gameObject);
            powerUpEffect.Apply(collision.gameObject);
        }
    }
}
