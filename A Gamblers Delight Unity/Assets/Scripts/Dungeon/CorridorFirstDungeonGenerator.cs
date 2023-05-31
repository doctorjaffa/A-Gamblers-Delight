using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Base class is SimpleRandomWalkDungeonGenerator to use the RunRandomWalk method.
public class CorridorFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField]
    private int corridorLength = 20, corridorCount = 10; // Corridor parameters.
    [SerializeField]
    [Range(0.1f, 1)]
    private float roomPercent = 0.6f;   // How much of the potential room positions should be used to actually create rooms.

    // On scene load, generate a random dungeon from this method.
    private void Awake()
    {
        tilemapVisualizer.Clear();
        GenerateDungeon();
    }

    // Overrides the base class method.
    protected override void RunProceduralGeneration()
    {
        // Call the CorridorFirstGeneration method.
        CorridorFirstGeneration();

        Instantiate(player);
        player.transform.position.Set(startPosition.x, startPosition.y, 0);
    }

    // Generate a corridor.
    private void CorridorFirstGeneration()
    {
        // Create a new HashSet for floor positions.
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        // Create a new HashSet for potential room postions.
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();

        // Call CreateCorridors, passing in the floor positions.
        CreateCorridors(floorPositions, potentialRoomPositions);

        // Create a hash set of room positions. 
        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);

        // Create a list of all dead ends in the dungeon generation.
        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);

        CreateRoomsAtDeadEnd(deadEnds, roomPositions);

        // Create corridors and rooms with no duplicates.
        floorPositions.UnionWith(roomPositions);

        //Paint the tiles accordingly.
        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
    }

    // Create rooms at the dead ends, even if they have gone over the room percent.
    private void CreateRoomsAtDeadEnd(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
    {
        // For each position in dead ends.
        foreach (Vector2Int position in deadEnds)
        {
            // If there is not a room in this position.
            if (roomFloors.Contains(position) == false)
            {
                // Create a randomly generated room at this position. 
                HashSet<Vector2Int> room = RunRandomWalk(randomWalkParamters, position);
                // Add the new room to the hash set, without duplicating positions.
                roomFloors.UnionWith(room);
            }
        }
    }

    // Find any dead ends throughout the generation.
    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        // Create a list for dead ends.
        List<Vector2Int> deadEnds = new List<Vector2Int>();

        // For each position in floor positions.
        foreach (Vector2Int position in floorPositions)
        {
            // Create a temporary variable.
            int neighboursCount = 0;

            // For each direction in the cardinal directions.
            foreach (Vector2Int direction in Direction2D.cardinalDirectionsList)
            {
                // If there is a floor position stored in each direction.
                if(floorPositions.Contains(position + direction))
                {
                    // There is a floor there, so add to the neigbours count.
                    neighboursCount++;
                }
            }

            // If the neighbours count == 1, this position is a dead end.
            if (neighboursCount == 1)
            {
                // Add this position to the dead ends hash set.
                deadEnds.Add(position);
            }
        }

        // Return dead ends.
        return deadEnds;
    }

    // Create rooms.
    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPositions)
    {
        // Create a new hash set to store room positions.
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();

        // Calculate the count of rooms to select from potential positions.
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * roomPercent);

        // Sort the potential room positions and extract the list of room positions taken at random.
        // Global Unique ID given to each room to randomly sort the hash set.
        List<Vector2Int> roomsToCreate = potentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();

        // Loop through each position and create rooms at those.
        foreach (Vector2Int roomPostion in roomsToCreate)
        {
            HashSet<Vector2Int> roomFloor = RunRandomWalk(randomWalkParamters, roomPostion);
            roomPositions.UnionWith(roomFloor);
        }

        return roomPositions;
    }

    // Creates the corridor tile positions.
    private void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
    {
        // Set the current position equal to start position.
        Vector2Int currentPosition = startPosition;

        potentialRoomPositions.Add(currentPosition);

        // Until i >= corridorCount.
        for (int i = 0; i < corridorCount; i++)
        {
            // Generate corridor positions.
            List<Vector2Int> corridor = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPosition, corridorLength);
            // Ensures corridors are connected.
            currentPosition = corridor[corridor.Count - 1];

            // Add each end of a corridor to the potential rooms HashSet.
            potentialRoomPositions.Add(currentPosition);

            // Ensures no duplicates are added to the floorPositions HashSet.
            floorPositions.UnionWith(corridor);
        }
    }
}
