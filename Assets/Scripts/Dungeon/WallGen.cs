/*
 * Source Code Originally by SunnyValleyStudio
 * Modified for usage by Ho-Sik Choo
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGen
{
   public static HashSet<Vector2Int> CreateWalls(HashSet<Vector2Int> floorPos, TileMapVisualizer tileMap)
   {
      HashSet<Vector2Int> retWallPositions = new HashSet<Vector2Int>();
      HashSet<Vector2Int> wallPos          = FindWallDirs(floorPos, Direction2D.dirList);
      HashSet<Vector2Int> cornerWallPos    = FindWallDirs(floorPos, Direction2D.diagonalDirList);
      retWallPositions.UnionWith(wallPos);
      retWallPositions.UnionWith(cornerWallPos);
      
      CreateBasicWalls(tileMap, wallPos, floorPos);
      CreateCornerWalls(tileMap, cornerWallPos, floorPos);

      return retWallPositions;
   }

   private static void CreateCornerWalls(TileMapVisualizer tileMap, HashSet<Vector2Int> cornerWallPos, HashSet<Vector2Int> floorPos)
   {
      foreach (var position in cornerWallPos)
      {
         string neighborsBinType = "";
         foreach (var dir in Direction2D.eightDirList)
         {
            Vector2Int neighborPos = position + dir;
            if (floorPos.Contains(neighborPos))
               neighborsBinType += "1";
            else
               neighborsBinType += "0";
         }

         tileMap.PaintSingleCornerWall(position, neighborsBinType);
      }
   }

   private static void CreateBasicWalls(TileMapVisualizer tileMap, HashSet<Vector2Int> wallPos, HashSet<Vector2Int> floorPos)
   {
      foreach (var position in wallPos)
      {
         string neighborsBinType = "";
         foreach (var dir in Direction2D.dirList)
         {
            Vector2Int neighborPos = position + dir;
            if (floorPos.Contains(neighborPos))
               neighborsBinType += "1";
            else
               neighborsBinType += "0";
         }
         tileMap.PaintSingleWall(position, neighborsBinType);
      }
   }

   private static HashSet<Vector2Int> FindWallDirs(HashSet<Vector2Int> floorPos, List<Vector2Int> dirList)
   {
      HashSet<Vector2Int> retWallPos = new HashSet<Vector2Int>();

      foreach (Vector2Int position in floorPos)
      {
         foreach (Vector2Int dir in dirList)
         {
            Vector2Int pos = position + dir;
            if (floorPos.Contains(pos) == false)
            {
               retWallPos.Add(pos);
            }
         }
      }
      return retWallPos;
   }
   
}
