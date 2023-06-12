using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAttack : MonoBehaviour
{
    [SerializeField]
    private float animationDuration;
    [SerializeField]
    private Animator animator;

    private float timer;

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
            animator.SetBool("isAttacking", true);
            ///Debug.Log("Collided with player!");
            // Deal damage to the player
            playerGambleScript.GambleCoins();

        }
    }

    private void Update()
    {
        if (timer <= animationDuration)
        {
            timer += Time.deltaTime;
        }
        else
        {
            animator.SetBool("isAttacking", false);
            timer = 0;
        }
    }
}
