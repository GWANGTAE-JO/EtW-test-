/*
 * Source Code Originally by SunnyValleyStudio
 * Modified for usage by Ho-Sik Choo
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapVisualizer : MonoBehaviour
{
   [SerializeField] private Tilemap        _floorTileMap, _wallTileMap;
   [SerializeField] public  TileBase[] floorTiles = new TileBase[4];
   [SerializeField] private TileBase _floorTile, _wallTile, _wallSideRightTile, _wallSideLeftTile, _wallBottomTile, _wallFullTile,
                                     _wallInnerCornerDownLeftTile, _wallInnerCornerDownRightTile, _wallDiagonalCornerDownLeftTile, _wallDiagonalCornerDownRightTile, 
                                     _wallDiagonalCornerUpLeftTile, _wallDiagonalCornerUpRightTile;

   public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions, ePlayerLocation dungeonNumber)
   {
      switch (dungeonNumber)
      {
         case ePlayerLocation.Dungeon0:
            _floorTile = floorTiles[0];
            break;
         case ePlayerLocation.Dungeon1:
            _floorTile = floorTiles[1];
            break;
         case ePlayerLocation.Dungeon2:
            _floorTile = floorTiles[2];
            break;
         case ePlayerLocation.Dungeon3:
            _floorTile = floorTiles[3];
            break;
         default:
            Debug.Log("Undefined Dungeon, Called from TileMapVisualizer.PaintFloorTiles");
            break;
      }
      
      PaintTiles(floorPositions, _floorTileMap, _floorTile);
   }

   public void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tileMap, TileBase tileBase)
   {
      foreach (var pos in positions)
      {
         PaintSingleTile(pos, tileMap, tileBase);
      }
   }

   private void PaintSingleTile(Vector2Int pos, Tilemap tileMap, TileBase tileBase)
   {
      var tilePos = tileMap.WorldToCell((Vector3Int)pos);
      tileMap.SetTile(tilePos, tileBase);
   }

   public void Clear()
   {
      _floorTileMap.ClearAllTiles();
      _wallTileMap.ClearAllTiles();
   }

   public void PaintSingleWall(Vector2Int wall, string binType)
   {
      TileBase tile      = null;
      int      typeAsInt = Convert.ToInt32(binType, 2);
      if (WallTypeHelper.wallTop.Contains(typeAsInt))
      {
         tile = _wallTile;
      }
      else if (WallTypeHelper.wallSideLeft.Contains(typeAsInt))
      {
         tile = _wallSideLeftTile;
      }
      else if (WallTypeHelper.wallSideRight.Contains(typeAsInt))
      {
         tile = _wallSideRightTile;
      }
      else if (WallTypeHelper.wallBottm.Contains(typeAsInt))
      {
         tile = _wallBottomTile;
      }
      else if (WallTypeHelper.wallFull.Contains(typeAsInt))
      {
         tile = _wallFullTile;
      }
      
      if(tile != null)
         PaintSingleTile(wall, _wallTileMap, tile);
   }

   public void PaintSingleCornerWall(Vector2Int wall, string neighborsBinType)
   {
      TileBase tile      = null;
      int      typeAsInt = Convert.ToInt32(neighborsBinType, 2);
      if (WallTypeHelper.wallInnerCornerDownLeft.Contains(typeAsInt))
      {
         tile = _wallInnerCornerDownLeftTile;
      }
      else if (WallTypeHelper.wallInnerCornerDownRight.Contains(typeAsInt))
      {
         tile = _wallInnerCornerDownRightTile;
      }
      else if (WallTypeHelper.wallDiagonalCornerDownLeft.Contains(typeAsInt))
      {
         tile = _wallDiagonalCornerDownLeftTile;
      }
      else if (WallTypeHelper.wallDiagonalCornerDownRight.Contains(typeAsInt))
      {
         tile = _wallDiagonalCornerDownRightTile;
      }
      else if (WallTypeHelper.wallDiagonalCornerUpLeft.Contains(typeAsInt))
      {
         tile = _wallDiagonalCornerUpLeftTile;
      }
      else if (WallTypeHelper.wallDiagonalCornerUpRight.Contains(typeAsInt))
      {
         tile = _wallDiagonalCornerUpRightTile;
      }
      else if (WallTypeHelper.wallFullEightDirections.Contains(typeAsInt))
      {
         tile = _wallFullTile;
      }
      else if (WallTypeHelper.wallBottmEightDirections.Contains(typeAsInt))
      {
         tile = _wallBottomTile;
      }
      
      if(tile != null)
         PaintSingleTile(wall, _wallTileMap, tile);
   }
}
