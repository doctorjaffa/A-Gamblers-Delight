using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Serialized Variables
    // Amount of damage the coin does
    [SerializeField]
    private int coinDamage = 13;

    //Private variables
    // Objects in the scene
    private Camera mainCamera;
    private Rigidbody2D rigidBody;

    // Where the mouse is
    private Vector3 mousePosition;

    // Direction and rotation of the object
    private Vector3 direction;
    private Vector3 rotation;

    // How fast the coin moves
    private float coinSpeed = 50;

    // Timer 
    private float timer;
    // How long a coin travels
    private float coinTravelTime = 2;

    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //
    // ------------------------------------------------------------------------------------------- GETTER/SETTER FUNCTIONS ---------------------------------------------------------------------------------------------- //
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //

    // Set the amount of damage a coin does
    public void SetCoinDamage(int amount)
    {
        coinDamage += amount;
    }

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

        // Set velocity of projectile in direction of mouse
        rigidBody.velocity = new Vector2(direction.x, direction.y).normalized * coinSpeed;

        float projectileRotation = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, projectileRotation);
    }

    private void Update()
    {
        // Run a timer
        timer += Time.deltaTime;

        // When the timer passes the coin travel time, destroy the coin
        if (timer > coinTravelTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        //Debug.Log("Collision Detected");

        EnemyHealth enemyHealthScript = collision.gameObject.GetComponent<EnemyHealth>();

        if (enemyHealthScript)
        {
            //Debug.Log("Collided with enemy!");
            // Deal damage to the enemy
            enemyHealthScript.ChangeHealth(-coinDamage);
            // Destroy the coin object
        }
    }

    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //
    // ------------------------------------------------------------------------------------------------ END OF FUNCTIONS ------------------------------------------------------------------------------------------------ //
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //
}
