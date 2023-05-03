using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Add the input system library to this script 
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Private Variables
    [SerializeField]
    // Player speed
    private float speed;

    // Empty rigid body
    private Rigidbody2D physicsBody = null;

    // Object movement inputs and the smoothed velocity
    private Vector2 movementInput;
    private Vector2 smoothedMovementInput;
    private Vector2 movementInputSmoothVelocity;

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
    }

    // Every time an input matching the given keybinds in InputManager's Move controls
    private void OnMove(InputValue inputValue)
    {
        // Identify what input is being used and apply the appropriate Vector2 to the movement input 
        movementInput = inputValue.Get<Vector2>();
    }
}
