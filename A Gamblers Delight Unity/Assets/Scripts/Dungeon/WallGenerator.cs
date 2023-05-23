using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Find walls surrounding the dungeon.
public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector2Int> floorPositions, TilemapVisualizer tilemapVisualizer)
    {
        HashSet<Vector2Int> basicWallPositions = FindWallsInDirections(floorPositions, Direction2D.cardinalDirectionsList);
        HashSet<Vector2Int> cornerWallPositions = FindWallsInDirections(floorPositions, Direction2D.diagonalDirectionsList);

        CreateBasicWalls(tilemapVisualizer, basicWallPositions, floorPositions);
        CreateCornerWalls(tilemapVisualizer, cornerWallPositions, floorPositions);
    }

    private static void CreateCornerWalls(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> cornerWallPositions, HashSet<Vector2Int> floorPositions)
    {
        foreach (var position in cornerWallPositions)
        {
            string neighboursBinaryType = "";

            foreach (var direction in Direction2D.eightDirectionsList)
            {
                var neigbourPosition = position + direction;
                if (floorPositions.Contains(neigbourPosition))
                {
                    neighboursBinaryType += "1";
                }
                else
                {
                    neighboursBinaryType += "0";
                }
            }

            tilemapVisualizer.PaintSingleCornerWall(position, neighboursBinaryType);
        }
    }

    // Create a basic wall using cardinal directions.
    private static void CreateBasicWalls(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> basicWallPositions, HashSet<Vector2Int> floorPositions)
    {
        // For each position in the basic walls hash set.
        foreach (Vector2Int position in basicWallPositions)
        {
            // Create the empty string to process the binary value of this wall.
            string neighboursBinaryType = "";

            // For each direction in the four cardinal directions.
            foreach (Vector2Int direction in Direction2D.cardinalDirectionsList)
            {
                // Get the neighbouring position.
                Vector2Int neigbourPosition = position + direction;
                // If the neighbouring position is a floor position.
                if (floorPositions.Contains(neigbourPosition))
                {
                    // Add a 1 to the binary type.
                    neighboursBinaryType += "1";
                }
                else
                {
                    // Else, add 0.
                    neighboursBinaryType += "0";
                }
            }

            tilemapVisualizer.PaintSingleBasicWall(position, neighboursBinaryType);
        }
    }

    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionList)
    {
        // Create a new HashSet of Vector2Int.
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();

        // For each position in floorPositions.
        foreach (Vector2Int position in floorPositions)
        {
            // For each of the cardinal directions.
            foreach (Vector2Int direction in directionList)
            {
                // Get the position next to this current position in each direction.
                Vector2Int neighbourPosition = position + direction;
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
