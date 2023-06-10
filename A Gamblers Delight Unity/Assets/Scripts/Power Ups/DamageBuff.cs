using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/DamageBuff")]
public class DamageBuff : PowerUpEffect
{

    public int amount;

    public override void Apply(GameObject target)
    {
        target.GetComponent<Projectile>().SetCoinDamage(amount);
    }
}
