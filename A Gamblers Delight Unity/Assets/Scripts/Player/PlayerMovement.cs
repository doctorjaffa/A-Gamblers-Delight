using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Add the input system library to this script 
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //
    // -------------------------------------------------------------------------------------------- VARIABLES INITIALISATION -------------------------------------------------------------------------------------------- //
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //

    // Serialized Variables
    [SerializeField]
    // Player speed
    private float speed;

    // Player animator
    [SerializeField]
    private Animator animator;

    // Private variables
    // Empty rigid body
    private Rigidbody2D physicsBody = null;

    // Object movement inputs and the smoothed velocity
    private Vector2 movementInput;
    private Vector2 smoothedMovementInput;
    private Vector2 movementInputSmoothVelocity;

    // Used to determine which way the player is facing
    private bool facingRight = true;

    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //
    // ---------------------------------------------------------------------------------------------------  FUNCTIONS --------------------------------------------------------------------------------------------------- //
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //

    // On awake, run this code 
    private void Awake()
    {
        // Get the rigid body attached to the object
        physicsBody = GetComponent<Rigidbody2D>();
    }

    // Per physics update tick
    private void FixedUpdate()
    {
        // Use the SmoothDamp function to smoothen object movement, based on user input and done over the course of 0.1s
        smoothedMovementInput = Vector2.SmoothDamp(smoothedMovementInput, movementInput, ref movementInputSmoothVelocity, 0.1f);
        // Set the velocity on the rigid body based on the smoothened input and the given object speed
        physicsBody.velocity = smoothedMovementInput * speed;

        // If the player is moving RIGHT but is facing LEFT
        if (smoothedMovementInput.x > 0 && !facingRight)
        {
            // Flip the sprite
            Flip();
        } 
        // Else, if the player is moving LEFT but is facing RIGHT 
        else if (smoothedMovementInput.x < 0 && facingRight)
        {
            // Flip the player sprite
            Flip();
        }

        if (physicsBody.velocity == Vector2.zero)
        {
            animator.SetBool("isRunning", false);
        }
    }

    // Every time an input matching the given keybinds in InputManager's Move controls
    private void OnMove(InputValue inputValue)
    {
        animator.SetBool("isRunning", true);

        // Identify what input is being used and apply the appropriate Vector2 to the movement input 
        movementInput = inputValue.Get<Vector2>();

    }

    // Flip the player sprite on the X axis
    private void Flip()
    {
        // Set facingRight to opposite of what it is
        facingRight = !facingRight;

        // Set a temporary scale and multiply the player by it 
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //
    // ------------------------------------------------------------------------------------------------ END OF FUNCTIONS ------------------------------------------------------------------------------------------------ //
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //
}
