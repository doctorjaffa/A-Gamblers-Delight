using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap, wallTilemap;   // Create a new tilemap variable.
    [SerializeField]
    private TileBase floorTile, wallTop;     // Create a new tilebase variable - stores the tile texture.

    // Paint each floor tile in the tilemap. 
    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions, floorTilemap, floorTile);
    }

    // Paint a wall tile in the given position.
    internal void PaintSingleBasicWall(Vector2Int position)
    {
        PaintSingleTile(wallTilemap, wallTop, position);
    }

    // Calls the method to set each individual tile in the tilemap.
    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (Vector2Int position in positions)
        {
            PaintSingleTile(tilemap, tile, position);
        }
    }

    // Paints a tile.
    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        // Finds the position of this tile position in the game world.
        Vector3Int tilePosition = tilemap.WorldToCell((Vector3Int)position);
        // Sets the tile to the tile texture provided. 
        tilemap.SetTile(tilePosition, tile);
    }

    // Clears all tilemaps.
    public void Clear()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }
}
