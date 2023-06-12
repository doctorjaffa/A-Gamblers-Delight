using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    //Private variables
    // Objects in the scene
    private Camera mainCamera;
    private Rigidbody2D rigidBody;
    private GameObject target;

    // Direction and rotation of the object
    private Vector3 direction;
    private Vector3 rotation;

    // How fast the coin moves
    private float arrowSpeed = 15;

    // Timer 
    private float timer;
    // How long a coin travels
    private float arrowTravelTime = 2;

    // Start is called before the first frame update
    void Start()
    {
        // Find the camera in the game world
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        // Find the player object in the scene
        target = GameObject.FindGameObjectWithTag("Player");

        // Get RigidBody
        rigidBody = GetComponent<Rigidbody2D>();

        direction = target.transform.position - transform.position;
        rotation = transform.position - target.transform.position;

        // Set velocity of projectile in direction of player
        rigidBody.velocity = new Vector2(direction.x, direction.y).normalized * arrowSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Run a timer
        timer += Time.deltaTime;

        // When the timer passes the coin travel time, destroy the coin
        if (timer > arrowTravelTime)
        {
            Destroy(gameObject);
        }
    }

    // When this object collides with another object
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collision Detected");

        // Get health component from the object
        PlayerGamble playerGambleScript = FindObjectOfType<PlayerGamble>();

        // If the object has collided with the player
        if (playerGambleScript)
        {
            ///Debug.Log("Collided with player!");
            // Deal damage to the player
            playerGambleScript.GambleCoins();

        }
    }
}
