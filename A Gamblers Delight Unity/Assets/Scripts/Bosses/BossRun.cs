using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRun : StateMachineBehaviour
{
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //
    // -------------------------------------------------------------------------------------------- VARIABLES INITIALISATION -------------------------------------------------------------------------------------------- //
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //

    // Serialized variables
    // Boss speed
    [SerializeField]
    private float speed = 2.5f;

    // Private variables
    // Player object
    private GameObject player;
    // Empty rigidbody
    private Rigidbody2D thisRigidbody;

    // Attack ranges
    private float biteAttackRange = 3;
    private float lungeAttackRange = 7;

    // The flip script 
    BossFlip bossFlip;

    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //
    // ---------------------------------------------------------------------------------------------------  FUNCTIONS --------------------------------------------------------------------------------------------------- //
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Find the player in the game scene
        player = GameObject.FindGameObjectWithTag("Player");

        // Find these components on this object
        thisRigidbody = animator.GetComponent<Rigidbody2D>();
        bossFlip = animator.GetComponent<BossFlip>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Flip the boss to face the player 
        bossFlip.LookAtPlayer();

        // Find the player position and create a new position going towards the player 
        Vector2 target = new Vector2(player.transform.position.x, player.transform.position.y);
        Vector2 newPosition = Vector2.MoveTowards(thisRigidbody.position, target, speed * Time.fixedDeltaTime);

        // Move towards the player
        thisRigidbody.MovePosition(newPosition);

        // If the player is within biting range and a 1/10 chance is true
        if (Vector2.Distance(player.transform.position, thisRigidbody.position) <= lungeAttackRange && Random.Range(0, 25) == Random.Range(0, 25))
        {
            // Run the lunge animation
            animator.SetTrigger("Lunge");
        }
        // Otherwise, if the player is within bite range
        else if (Vector2.Distance(player.transform.position, thisRigidbody.position) <= biteAttackRange)
        {
            // If a 1/4 check is true
            if (Random.Range(0, 4) == Random.Range(0, 4))
            {
                // Run the slamming animation
                animator.SetTrigger("Slam");
            }
            else
            {
                // Otherwise, run the bite animation
                animator.SetTrigger("Bite");
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Reset the triggers so the boss does not loop an attack
        animator.ResetTrigger("Bite");
        animator.ResetTrigger("Lunge");
        animator.ResetTrigger("Slam");
    }

    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //
    // ------------------------------------------------------------------------------------------------ END OF FUNCTIONS ------------------------------------------------------------------------------------------------ //
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ //
}
