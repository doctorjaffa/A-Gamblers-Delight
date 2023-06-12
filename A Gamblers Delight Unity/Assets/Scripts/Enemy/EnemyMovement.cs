using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : ObjectMovement
{
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //
    // -------------------------------------------------------------------------------------------- VARIABLES INITIALISATION -------------------------------------------------------------------------------------------- //
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //

    // The thing the enemy should chase after
    [SerializeField]
    private GameObject target;

    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //
    // ---------------------------------------------------------------------------------------------------  FUNCTIONS --------------------------------------------------------------------------------------------------- //
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //

    // Awake is called when the script is loaded
    void Awake()
    {
        // Find the player object in the scene
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Move in the direction of our target

        animator.SetBool("isRunning", true);

        // Get the direction
        // Subtract the current position from the target position to get a distance vector
        // Normalise changes it to be length 1, so we can then multiply it by our speed / force
        Vector2 direction = ((Vector2)target.transform.position - (Vector2)transform.position).normalized;

        // Move in the correct direction with the set force strength
        thisRigidyBody.AddForce(direction * forceStrength);

        // If the player is further left on the screen than this object AND the object is facing right, flip the sprite to face left
        if (target.transform.position.x < transform.position.x && facingRight)
        {
            Flip();
        }
        // Else, if the player is further right on the screen than this object AND the object is facing left, flip the sprite to face right
        else if (target.transform.position.x > transform.position.x && !facingRight)
        {
            Flip();
        }
    }

    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //
    // ------------------------------------------------------------------------------------------------ END OF FUNCTIONS ------------------------------------------------------------------------------------------------ //
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //
}
