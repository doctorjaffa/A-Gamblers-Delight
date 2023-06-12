using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // Serialized variables
    // Distance the player should be for this enemy to change behaviour
    [SerializeField]
    private float distanceForDecision;

    // How fast an enemy can shoot
    [SerializeField]
    private float fireResetTime = 1.8f;

    // The thing the enemy should be targeting
    [SerializeField]
    private GameObject target;

    // Projectile prefab
    [SerializeField]
    private GameObject Arrow;

    // Private variables
    // The rigidbody attached to this object
    private Rigidbody2D ourRigidbody;

    // Boolean to mark if a player can shoot
    private bool canFire;
    // A timer
    private float timer;

    // Awake is called when the script is loaded
    void Awake()
    {
        // Get the rigidbody that we'll be using for movement
        ourRigidbody = GetComponent<Rigidbody2D>();

        // Find the player object in the scene
        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        // How far away are we from the target?
        float distance = ((Vector2)target.transform.position - (Vector2)transform.position).magnitude;

        if (distance <= distanceForDecision && canFire)
        {
            FireProjectile();
        }

        // Allow player to use weapon again after enough time has passed
        if (!canFire)
        {
            // Start timer
            timer += Time.deltaTime;

            // Check if enough time has passed
            if (timer > fireResetTime)
            {
                // Set canFire to true
                canFire = true;

                // Reset the timer
                timer = 0;
            }
        }
    }

    private void FireProjectile()
    {
        // Create a copy of the weapon when it is fired
        Instantiate(Arrow, transform.position, Quaternion.identity);
    }
}
