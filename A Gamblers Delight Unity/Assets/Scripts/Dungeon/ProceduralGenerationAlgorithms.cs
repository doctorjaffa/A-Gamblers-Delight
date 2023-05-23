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

    // Method used to randomly pick one of the directions within the above list.
    public static Vector2Int GetRandomCardinalDirection()
    {
        // Return a random direction.
        return cardinalDirectionsList[Random.Range(0, cardinalDirectionsList.Count)];
    }
}
