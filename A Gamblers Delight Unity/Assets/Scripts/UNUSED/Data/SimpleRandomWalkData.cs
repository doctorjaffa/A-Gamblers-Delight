using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ScriptableObject allows this script to be created in the inspector.
// Create asset menu allows the use of PCG files with a preset name in this object, with predefined data for each variable. 
// This works similar to a prefab, but this is purely paramaters to be used by the dungeon generator.
[CreateAssetMenu(fileName = "SimpleRandomWalkParameters_",menuName = "PCG/SimpleRandomWalkData")]
public class SimpleRandomWalkData : ScriptableObject
{
    public int iterations = 10, walkLength = 10;
    public bool startRandomlyEachIteration = true;
}
