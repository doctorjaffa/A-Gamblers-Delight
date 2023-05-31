using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Abstract class, so it cannot be created. 
// Child classes inherit from this script. 
// Every variable in this class should not have a value - it should be empty, or zero. 
public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    // SerializeField allows variables to be customised within the Unity Inspector - but not changed by other scripts. 
    [SerializeField]
    protected TilemapVisualizer tilemapVisualizer = null;   // Reference to the TilemapVisualizer script.
    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero;   // Start position.
    [SerializeField]
    protected GameObject player = null;     // Player object.

    // Calls the method to clear the tilemap, then calls the method to generate a new dungeon layout.
    public void GenerateDungeon()
    {
        tilemapVisualizer.Clear();
        RunProceduralGeneration();
    }

    // Abstract method which can be created in child classes. 
    protected abstract void RunProceduralGeneration();
}
