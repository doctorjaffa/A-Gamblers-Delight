using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/FiringSpeedBuff")]
public class FiringSpeedBuff : PowerUpEffect
{
    public float amount;

    public override void Apply(GameObject target)
    {
        target.GetComponent<PlayerAttack>().SetFiringSpeed(amount);
    }
}
