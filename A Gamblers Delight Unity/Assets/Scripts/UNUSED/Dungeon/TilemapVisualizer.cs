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
    private TileBase floorTile, wallTop, wallSideRight, wallSideLeft, wallBottom, wallFull,
        wallInnerCornerDownLeft, wallInnerCornerDownRight, 
        wallDiagonalCornerDownRight, wallDiagonalCornerDownLeft, wallDiagonalCornerUpRight, wallDiagonalCornerUpLeft; // Create a new tilebase variable - stores the tile texture.

    // Paint each floor tile in the tilemap. 
    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions, floorTilemap, floorTile);
    }

    // Paint a wall tile in the given position.
    internal void PaintSingleBasicWall(Vector2Int position, string binaryType)
    {
        // Convert the string to an integer with two different values.
        int typeAsInt = Convert.ToInt32(binaryType, 2);

        TileBase tile = null;

        // --------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
        // This section is a block of if statements to determine which binary number in the WallTypesHelper script matches the binaryType passed into this method. 
        if (WallTypesHelper.wallTop.Contains(typeAsInt))
        {
            tile = wallTop;
        }
        else if (WallTypesHelper.wallSideRight.Contains(typeAsInt))
        {
            tile = wallSideRight;
        }
        else if (WallTypesHelper.wallSideLeft.Contains(typeAsInt))
        {
            tile = wallSideLeft;
        }
        else if (WallTypesHelper.wallBottom.Contains(typeAsInt))
        {
            tile = wallBottom;
        }
        else if (WallTypesHelper.wallFull.Contains(typeAsInt))
        {
            tile = wallFull;
        }

        // If the tile has been filled, paint the tile using the given result in the position.

        if (tile != null)
        {
            PaintSingleTile(wallTilemap, tile, position);
        }

        // --------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
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

    internal void PaintSingleCornerWall(Vector2Int position, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);

        TileBase tile = null;

        // --------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
        // This section is a block of if statements to determine which binary number in the WallTypesHelper script matches the binaryType passed into this method. 

        if (WallTypesHelper.wallInnerCornerDownLeft.Contains(typeAsInt))
        {
            tile = wallInnerCornerDownLeft;
        }
        else if (WallTypesHelper.wallInnerCornerDownRight.Contains(typeAsInt))
        {
            tile = wallInnerCornerDownRight;
        }
        else if (WallTypesHelper.wallDiagonalCornerDownLeft.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerDownLeft;
        }
        else if (WallTypesHelper.wallDiagonalCornerDownRight.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerDownRight;
        }
        else if (WallTypesHelper.wallDiagonalCornerUpRight.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerUpRight;
        }
        else if (WallTypesHelper.wallDiagonalCornerUpLeft.Contains(typeAsInt))
        {
            tile = wallDiagonalCornerUpLeft;
        }
        else if (WallTypesHelper.wallFullEightDirections.Contains(typeAsInt))
        {
            tile = wallFull;
        }
        else if (WallTypesHelper.wallBottmEightDirections.Contains(typeAsInt))
        {
            tile = wallBottom;
        }

        // If the tile has been filled, paint the tile using the given result in the position.

        if (tile != null)
        {
            PaintSingleTile(wallTilemap, tile, position);
        }

        // --------------------------------------------------------------------------------------------------------------------------------------------------------------------- //
    }

    // Clears all tilemaps.
    public void Clear()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }
}
