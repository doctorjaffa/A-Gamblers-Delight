using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Add the input system library to this script 
using UnityEngine.InputSystem;

public class PlayerMovement : ObjectMovement
{
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //
    // -------------------------------------------------------------------------------------------- VARIABLES INITIALISATION -------------------------------------------------------------------------------------------- //
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //
    
    //Private Variables
    // The movement direction
    private Vector2 movementDirection;
    // The float value of the horizontal movement (On the X-axis)
    private float horizontalMove;

    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //
    // ---------------------------------------------------------------------------------------------------  FUNCTIONS --------------------------------------------------------------------------------------------------- //
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //

    private void Update()
    {
        // Get the input movement on the X axis
        horizontalMove = Input.GetAxis("Horizontal");
        // Create a new direction based on input
        movementDirection = new Vector2(horizontalMove, Input.GetAxis("Vertical"));

        // If the player is moving LEFT but is facing RIGHT
        if (horizontalMove < 0 && facingRight)
        {
            Flip();
        }
        // If the player is moving RIGHT but is facing LEFT
        else if (horizontalMove > 0 && !facingRight)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        // Update the velocity 
        thisRigidyBody.velocity = movementDirection * forceStrength;

        // If there is no movement
        if (thisRigidyBody.velocity == Vector2.zero)
        {
            // The player is not running
            animator.SetBool("isRunning", false);
        }
        else
        {
            // Else, the player is running
            animator.SetBool("isRunning", true);
        }
    }
}
