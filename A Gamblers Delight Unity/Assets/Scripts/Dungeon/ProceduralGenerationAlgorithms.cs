using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProceduralGenerationAlgorithms
{
    // Walk in a random cardinal direction.
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPostion, int walkLength)
    {
        // Create a new path.
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        // Add the start position to the new path.
        path.Add(startPostion);
        // Set the previous position to equal the starting position.
        Vector2Int previousPosition = startPostion;

        // Iterate until i >= walkLength.
        for (int i = 0; i < walkLength; i++)
        {
            // Set a new position using the previous position and walking in a random cardinal direction by one tile.
            Vector2Int newPosition = previousPosition + Direction2D.GetRandomCardinalDirection();
            // Add this new position to the path.
            path.Add(newPosition);
            // Update the previous position to be the position just created.
            previousPosition = newPosition;
        }

        // Return the finished path.
        return path;
    }

    // List because this corridor generation will not create duplicates, and the last position is needed to create the next room.
    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition, int corridorLength)
    {
        List<Vector2Int> corridor = new List<Vector2Int>();
        var direction = Direction2D.GetRandomCardinalDirection();
        var currentPositon = startPosition;
        corridor.Add(currentPositon);

        for (int i = 0; i < corridorLength; i++)
        {
            currentPositon += direction;
            corridor.Add(currentPositon);
        }

        return corridor;
    }
}

// Class of cardinal directions.
public static class Direction2D
{
    // List of the four cardinal directions in Vector2Int.
    public static List<Vector2Int> cardinalDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(0, 1),  // UP
        new Vector2Int(1, 0),  // RIGHT
        new Vector2Int(0, -1), // DOWN
        new Vector2Int(-1, 0)  // LEFT
    };

    // List of the four diagonal directions in Vector2Int.
    public static List<Vector2Int> diagonalDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(1, 1),   // UP-RIGHT
        new Vector2Int(1, -1),  // RIGHT-DOWN
        new Vector2Int(-1, -1), // DOWN-LEFT
        new Vector2Int(-1, 1)   // LEFT-UP
    };

    // List of all eight directions in Vector2Int.
    public static List<Vector2Int> eightDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(0, 1),   // UP
        new Vector2Int(1, 1),   // UP-RIGHT
        new Vector2Int(1, 0),   // RIGHT
        new Vector2Int(1, -1),  // RIGHT-DOWN
        new Vector2Int(0, -1),  // DOWN
        new Vector2Int(-1, -1), // DOWN-LEFT
        new Vector2Int(-1, 0),  // LEFT
        new Vector2Int(-1, 1)   // LEFT-UP
    };

    // Method used to randomly pick one of the directions within the above list.
    public static Vector2Int GetRandomCardinalDirection()
    {
        // Return a random direction.
        return cardinalDirectionsList[Random.Range(0, cardinalDirectionsList.Count)];
    }
}
