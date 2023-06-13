using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFlip : MonoBehaviour
{
    // Serialized variables
    // Player object
    [SerializeField]
    private GameObject player;

    // Private variables
    // Boolean to check if the object is flipped
    private bool isFlipped = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Look towards the player object
    public void LookAtPlayer()
    {
        // Get the scale of this object 
        Vector3 flipped = transform.localScale;
        // Flipped is the negative value of the local scale
        flipped.z *= -1f;

        // If this object is further RIGHT than the player AND the object is flipped
        if (transform.position.x > player.transform.position.x && isFlipped)
        {
            // Update the local scale
            transform.localScale = flipped;
            // Rotate this object to face the opposite direction
            transform.Rotate(0f, 180f, 0f);
            // Flipped is now false
            isFlipped = false;
        }
        // If this object is further LEFT than the player AND the object is NOT flipped
        else if (transform.position.x < player.transform.position.x && !isFlipped)
        {
            // Update the local scale
            transform.localScale = flipped;
            // Rotate this object to face the opposite direction
            transform.Rotate(0f, 180f, 0f);
            // Flipped is now true
            isFlipped = true;
        }
    }
}
