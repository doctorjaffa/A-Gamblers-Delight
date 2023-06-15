using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectProjectile : MonoBehaviour
{
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //
    // -------------------------------------------------------------------------------------------- VARIABLES INITIALISATION -------------------------------------------------------------------------------------------- //
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //

    // Serialized variables
    [SerializeField]
    // How long a coin travels
    protected float objectTravelTime;
    [SerializeField]
    // How fast the coin moves
    protected float objectSpeed;

    //Protected variables
    // Objects in the scene
    protected Rigidbody2D rigidBody;

    // Timer 
    protected float timer;

    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //
    // ---------------------------------------------------------------------------------------------------  FUNCTIONS --------------------------------------------------------------------------------------------------- //
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //

    // Start is called before the first frame update
    void Start()
    {
        // Get RigidBody
        rigidBody = GetComponent<Rigidbody2D>();

        if (rigidBody.CompareTag("Coin"))
        {
            // Set velocity of projectile
            rigidBody.velocity = new Vector2(Input.GetAxis("Horizontal"), (Input.GetAxis("Vertical"))) * objectSpeed;
        }
        else if (rigidBody.CompareTag("EnemyProjectile"))
        {
            // Get the direction this projectile should travel in
            Vector2 direction = FindTargetDirection();


            // Set velocity of projectile in direction of target
            rigidBody.velocity = new Vector2(direction.x, direction.y).normalized * objectSpeed;
        }
    }

    void Update()
    {
        // Run a timer
        timer += Time.deltaTime;

        // When the timer passes the coin travel time, destroy the coin
        if (timer > objectTravelTime)
        {
            Destroy(gameObject);
        }
    }

    private Vector2 FindTargetDirection()
    {
        GameObject target = GameObject.FindGameObjectWithTag("Player");

        Vector2 direction = target.transform.position - transform.position;

        return direction;
    }
}
