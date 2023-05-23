using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Find walls surrounding the dungeon.
public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector2Int> floorPositions, TilemapVisualizer tilemapVisualizer)
    {
        var basicWallPositions = FindWallsInDirections(floorPositions, Direction2D.cardinalDirectionsList);
        foreach (var position in basicWallPositions)
        {
            tilemapVisualizer.PaintSingleBasicWall(position);
        }
    }

    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionList)
    {
        // Create a new HashSet of Vector2Int.
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();

        // For each position in floorPositions.
        foreach (var position in floorPositions)
        {
            // For each of the cardinal directions.
            foreach (var direction in directionList)
            {
                // Get the position next to this current position in each direction.
                var neighbourPosition = position + direction;
                // If the floorPositions does NOT contain this neighbouring position, it must be a wall.
                if (floorPositions.Contains(neighbourPosition) == false)
                {
                    // Add the neighbouring positions to the wallPositions HashSet.
                    wallPositions.Add(neighbourPosition);
                }
            }
        }

        return wallPositions;
    }
}
