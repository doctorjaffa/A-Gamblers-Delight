using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Private variables

    // Object speed 
    [SerializeField]
    private float speed;

    // Object rotation speed
    [SerializeField]
    private float rotationSpeed;

    // Object rigidbody
    private Rigidbody2D rigidbody = null;
    
    // Object awareness controller (tells when player is close enough to object)
    private PlayerAwarnessController playerAwarnessController;

    // Object target direction (towards player object)
    private Vector2 targetDirection;

    // Awake is called upon object creation
    void Awake()
    {
        // Get Rigidbody attached to object
        rigidbody = GetComponent<Rigidbody2D>();
        
        // Get PlayerAwarenessController attached to object
        playerAwarnessController = GetComponent<PlayerAwarnessController>();

        // Get the animation that will be used 
        Animator enemyAnimtor = GetComponent<Animator>();
    }

    // FixedUpdate is called every physics tick
    private void FixedUpdate()
    {
        // Call each function in this script 
        UpdateTargetDirection();
        RotateTowardsTarget();
        SetVelocity();
    }

    // Moving towards the player 
    private void UpdateTargetDirection()
    {
        // If the player is close enough to this object
        if (playerAwarnessController.AwareOfPlayer)
        {
            // Update the target direction to the direction of the player 
            targetDirection = playerAwarnessController.DirectionToPlayer;
        }
        // If the player is not close enough, leave the target direction to zero
        else
        {
            targetDirection = Vector2.zero;
        }
    }

    // Start rotating this object to face the player 
    private void RotateTowardsTarget()
    {
        // If the player is not close enough, do not run the rest of this function
        if (targetDirection == Vector2.zero)
        {
            return;
        }

        // If the player is close enough, set the target rotation to the direction the player is in
        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, targetDirection);
        // Rotate this object towards the target, at a fixed speed
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Set the object rotation 
        rigidbody.SetRotation(rotation);
    }

    // Move this object if the player is close enough for it to be aware
    private void SetVelocity()
    {
        // If the player is not close enough, do not move 
        if (targetDirection == Vector2.zero)
        {
            // Make the enemy idle
            this.GetComponent<Animator>().SetBool("isRunning", false);

            rigidbody.velocity = Vector2.zero;
        }
        // If the player is close enough, begin moving 
        else
        {
            // Make the enemy start running
            this.GetComponent<Animator>().SetBool("isRunning", true);
            rigidbody.velocity = transform.up * speed;
        }
    }
}
