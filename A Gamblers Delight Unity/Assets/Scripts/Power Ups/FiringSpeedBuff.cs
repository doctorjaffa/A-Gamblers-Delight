using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Allows this object to be given to PowerUp script
[CreateAssetMenu(menuName = "PowerUps/FiringSpeedBuff")]
public class FiringSpeedBuff : PowerUpEffect
{
    private float amount;
    private GameObject target;

    public override void Apply(GameObject target)
    {
        // Find the player attack script 
        PlayerAttack attack = target.GetComponentInChildren<PlayerAttack>();
        // Update the firing speed
        attack.SetFiringSpeed(amount);
    }
}
