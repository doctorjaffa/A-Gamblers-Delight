using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerUpEffect powerUpEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        // Check for player. 
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            Destroy(gameObject);
            powerUpEffect.Apply(collision.gameObject);
        }
    }
}
