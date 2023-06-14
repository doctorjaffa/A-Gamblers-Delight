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
    protected Camera mainCamera;
    protected Rigidbody2D rigidBody;

    // Where the mouse is
    protected Vector3 mousePosition;

    // Direction and rotation of the object
    protected Vector3 direction;
    protected Vector3 rotation;

    // Timer 
    protected float timer;

    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //
    // ---------------------------------------------------------------------------------------------------  FUNCTIONS --------------------------------------------------------------------------------------------------- //
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //

    // Start is called before the first frame update
    void Start()
    {
        // Find the camera in the game world
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        // Get RigidBody
        rigidBody = GetComponent<Rigidbody2D>();

        // Get mouse position
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        direction = mousePosition - transform.position;
        rotation = transform.position - mousePosition;

        // Set velocity of projectile
        //rigidBody.velocity = new Vector2(direction.x, direction.y).normalized * objectSpeed;

        rigidBody.velocity = transform.position * objectSpeed;
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
}
