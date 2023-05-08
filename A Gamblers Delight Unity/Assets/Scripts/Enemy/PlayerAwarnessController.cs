using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAwarnessController : MonoBehaviour
{
    // Enemy is aware of the player?
    public bool AwareOfPlayer { get; private set; }

    // The direction of the player 
    public Vector2 DirectionToPlayer { get; private set; }

    // How close the player has to be for this object to be aware of it 
    [SerializeField]
    private float playerAwarenessDistance;

    // Player object
    private Transform player = null;

    // Awake is called on object creation
    void Awake()
    {
        // Find the player in the scene through its PlayerMovement script 
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Find how close the player is to this object
        Vector2 enemyToPlayerVector = player.position - transform.position;
        // No magnitude needed so normalize the vector for the direction only
        DirectionToPlayer = enemyToPlayerVector.normalized;

        // If the distance distance between the player and object (magnitude) is less than the awareness distance
        if (enemyToPlayerVector.magnitude <= playerAwarenessDistance)
        {
            // This object is aware of the player
            AwareOfPlayer = true;
        }
        // Otherwise, it is not aware
        else
        {
            AwareOfPlayer = false;
        }
    }
}
