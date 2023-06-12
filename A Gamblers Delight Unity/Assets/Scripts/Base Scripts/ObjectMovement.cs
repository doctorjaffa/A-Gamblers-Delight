using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectMovement : MonoBehaviour
{
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //
    // -------------------------------------------------------------------------------------------- VARIABLES INITIALISATION -------------------------------------------------------------------------------------------- //
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //

    // Serialized variables
    [SerializeField]
    // How fast the enemy moves
    protected float forceStrength;

    // Enemy animator
    [SerializeField]
    protected Animator animator;

    // Hidden variables
    // The rigidbody attached to this object
    protected Rigidbody2D thisRigidyBody;
    // Used to determine which way the player is facing
    protected bool facingRight = true;

    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //
    // ---------------------------------------------------------------------------------------------------  FUNCTIONS --------------------------------------------------------------------------------------------------- //
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //

    // Awake is called when the script is loaded
    void Start()
    {
        // Get the rigidbody that we'll be using for movement
        thisRigidyBody = GetComponent<Rigidbody2D>();
    }

    // Flip the enemy sprite on the X axis
    protected void Flip()
    {
        // Set facingRight to opposite of what it is
        facingRight = !facingRight;

        // Set a temporary scale and multiply the enemy by it 
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
