using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Declare Variables
    private Vector3 mousePosition;
    private Camera mainCamera;
    private Rigidbody2D rigidBody;
    private Vector3 direction;
    private Vector3 rotation;
    private float force = 50;
    private float timer;
    private float coinTravelTime = 2;
    private int coinDamage = 13;


    // Start is called before the first frame update.
    void Start()
    {
        // Find the camera in the game world.
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        // Get RigidBody.
        rigidBody = GetComponent<Rigidbody2D>();

        // Get mouse position.
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        direction = mousePosition - transform.position;
        rotation = transform.position - mousePosition;

        // Set velocity of projectile in direction of mouse.
        rigidBody.velocity = new Vector2(direction.x, direction.y).normalized * force;

        float projectileRotation = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, projectileRotation);
    }

    private void Update()
    {
        // Run a timer.
        timer += Time.deltaTime;

        // When the timer passes the coin travel time, destroy the coin.
        if (timer > coinTravelTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collision Detected");

        EnemyHealth enemyHealthScript = collision.gameObject.GetComponent<EnemyHealth>();

        if (enemyHealthScript)
        {
            //Debug.Log("Collided with enemy!");
            // Deal damage to the enemy.
            enemyHealthScript.ChangeHealth(-coinDamage);
            // Destroy the coin object.
            Destroy(gameObject);
        }
    }
}
