using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : ObjectProjectile
{
    // Serialized variables
    // Animation variables

    [SerializeField]
    private ObjectAnimator animator;

    // When this object collides with another object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Get health component from the object
        PlayerGamble playerGambleScript = FindObjectOfType<PlayerGamble>();

        // If the object has collided with the player
        if (playerGambleScript && collision.name == "Player")
        {
            Debug.Log("Hits player");
            Destroy(gameObject);
            //animator.ChangeBool("isAttacking");

            // Deal damage to the player
            playerGambleScript.GambleCoins();

        }
    }
}
