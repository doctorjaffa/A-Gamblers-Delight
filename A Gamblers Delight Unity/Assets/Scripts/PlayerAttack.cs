using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject Projectile;
    public bool canFire;
    public float timer;
    public float fireResetTime = 1;


    // Update is called once per frame
    void Update()
    {
        // Allow player to use weapon if enough time has passed
        if (Input.GetMouseButton(0) && canFire)
        {
            // Create a copy of the weapon when it is fired
            Instantiate(Projectile, transform);

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
}
