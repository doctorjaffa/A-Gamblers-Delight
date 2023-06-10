using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Public variables
    public GameObject Projectile;
    public bool canFire;
    public float timer;

    // Private variables
    private Camera mainCamera;
    private PlayerMovement playerMove;
    private Vector3 mousePosition;
    private Vector3 rotation;

    private float fireResetTime = 1;
    private float rotationZ;

    private Coins coins;

    private void Start()
    {
        playerMove = GetComponentInParent<PlayerMovement>();
        coins = GetComponentInParent<Coins>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        // Get calculate angle and convert to degrees
        rotationZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotationZ);

        // Allow player to use weapon if enough time has passed
        if (Input.GetMouseButton(0) && canFire)
        {
            // Shoot a coin 
            coins.ShootCoin();

            // Set canFire to false
            canFire = false;
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

    public void SetFiringSpeed(float amount)
    {
        fireResetTime -= amount;
    }
}
