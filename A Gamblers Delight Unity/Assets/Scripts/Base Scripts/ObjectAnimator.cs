using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAnimator : MonoBehaviour
{
    // Serialized variables
    // Animation variables
    [SerializeField]
    private float animationDuration;
    [SerializeField]
    private Animator animator;

    public void ChangeBool(string boolToChange)
    {
        if (animator.GetBool(boolToChange))
        {
            animator.SetBool(boolToChange, true);
        }
        else
        {
            animator.SetBool(boolToChange, false);
        }
    }

    public void TriggerAttack()
    {
        animator.Play("attack");
    }
}
