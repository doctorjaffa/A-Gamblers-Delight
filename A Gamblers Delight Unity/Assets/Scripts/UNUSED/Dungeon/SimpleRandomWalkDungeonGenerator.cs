using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

// This script creates procedural rooms. 
// Child class of AbstractDungeonGenerator
public class SimpleRandomWalkDungeonGenerator : AbstractDungeonGenerator
{
    // This gets the data stored in the SimpleRandomWalkData script, allowing the variables inside to be used in this script.
    [SerializeField]
    protected SimpleRandomWalkData randomWalkParamters;


    // Run the generation for each floor position in a given room/corridor.
    // This method is overridden from the AbstractDungeonGenerator base class. 
    protected override void RunProceduralGeneration()
    {
        // Set floor positions to equal the result of a random walk. 
        // HashSet is like an unordered list of related data - in this case, floor positions.
        HashSet<Vector2Int> floorPositions = RunRandomWalk(randomWalkParamters, startPosition);

        // Clear the tilemap, then paint the floor positions to have floor tiles visually appear on screen in those positions.
        tilemapVisualizer.Clear();
        tilemapVisualizer.PaintFloorTiles(floorPositions);

        // Create the walls using floorPositions, and passing the reference to the TilemapVisualizer script.
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
    }

    // Walks in a random direction for X amount of iterations. 
    // This method is reusable in child classes.
    protected HashSet<Vector2Int> RunRandomWalk(SimpleRandomWalkData parameters, Vector2Int position)
    {
        // Set the current position.
        Vector2Int currentPosition = position;
        // Set the floor positions to be a new empty HashSet of Vector 2 ints.
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

        // For each iteration in this generation, walk in a random direction.
        for (int i = 0; i < parameters.iterations; i++)
        {
            HashSet<Vector2Int> path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPosition, parameters.walkLength);
            // Add this new floor position to the path HashSet, without copying duplicates. 
            floorPositions.UnionWith(path);

            // Allow the next iteration to occur anywhere on the previously formed path, and not in a completely unconnected position.
            if (parameters.startRandomlyEachIteration)
            {
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
            }
        }

        // Return the updated floor positions. 
        return floorPositions;
    }

}
