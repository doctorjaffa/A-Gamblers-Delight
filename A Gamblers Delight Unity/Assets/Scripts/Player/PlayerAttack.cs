using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //
    // -------------------------------------------------------------------------------------------- VARIABLES INITIALISATION -------------------------------------------------------------------------------------------- //
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //

    // Serialized variables
    // Projectile prefab
    [SerializeField]
    private GameObject Projectile;
    // How fast a player can shoot
    [SerializeField]
    private float fireResetTime;

    // Player animator
    [SerializeField]
    private Animator animator;

    // Private variables
    // Different objects in the scene
    private Camera mainCamera;
    private PlayerMovement playerMove;
    private Coins coins;

    // The mouse position and rotations
    private Vector3 mousePosition;
    private Vector3 rotation;
    private float rotationZ;

    private Vector2 lookDirection;

    // Boolean to mark if a player can shoot
    private bool canFire;
    // A timer
    private float timer;

    // Serialized Variables
    // Amount of damage the coin does
    [SerializeField]
    private int coinDamage;

    private Rigidbody2D thisRigidbody;

    [SerializeField]
    private Transform firePoint;

    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //
    // ------------------------------------------------------------------------------------------- GETTER/SETTER FUNCTIONS ---------------------------------------------------------------------------------------------- //
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //

    // Set how fast the player can shoot
    public void SetFiringSpeed(float amount)
    {
        fireResetTime -= amount;
    }

    public void SetCoinDamage(int amount)
    {
        coinDamage += amount;
    }

    public int GetCoinDamage()
    {
        return coinDamage;
    }

    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //
    // ---------------------------------------------------------------------------------------------------  FUNCTIONS --------------------------------------------------------------------------------------------------- //
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //

    private void Start()
    {
        // Find the game objects/components within the scene
        playerMove = GetComponentInParent<PlayerMovement>();
        coins = GetComponentInParent<Coins>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        thisRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        // Get calculate angle and convert to degrees
        rotationZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotationZ);
        

        // Allow player to use weapon if enough time has passed
        if (Input.GetKey(KeyCode.Keypad5) && canFire)
        {

            // Set the attacking animation to true
            animator.SetBool("isAttacking", true);

            // Shoot a coin 
            coins.ShootCoin(firePoint);

            // Set canFire to false
            canFire = false;
        }
        else
        {
            animator.SetBool("isAttacking", false);
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

    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //
    // ------------------------------------------------------------------------------------------------ END OF FUNCTIONS ------------------------------------------------------------------------------------------------ //
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //
}
