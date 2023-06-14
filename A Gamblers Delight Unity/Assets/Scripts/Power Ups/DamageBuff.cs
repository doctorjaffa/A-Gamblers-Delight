using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Allows this object to be given to PowerUp script
[CreateAssetMenu(menuName = "PowerUps/DamageBuff")]
public class DamageBuff : PowerUpEffect
{

    private int amount;
    private GameObject target;

    public override void Apply(GameObject target)
    {
        // Find the player attack script 
        PlayerAttack attack = target.GetComponentInChildren<PlayerAttack>();
        // Update the coin damage
        attack.SetCoinDamage(amount);
    }
}
