using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Abstract script for PowerUp script to inherit from
public abstract class PowerUpEffect : ScriptableObject
{

    public abstract void Apply(GameObject target);
}
