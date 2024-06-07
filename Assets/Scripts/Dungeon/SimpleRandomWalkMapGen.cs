/*
 * Source Code Originally by SunnyValleyStudio
 * Modified for usage by Ho-Sik Choo
 */
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimpleRandomWalkMapGen : AbstractDungeonGen
{
   [SerializeField] protected RandomWalkSO _randomWalkSO;
   protected override void RunProceduralGen(ePlayerLocation dungeonNumber)
   {
      HashSet<Vector2Int> floorPos = RunRandomWalk(_randomWalkSO, _startPos);
      _tileMapVisualizer.Clear();
      _tileMapVisualizer.PaintFloorTiles(floorPos, dungeonNumber);
      WallGen.CreateWalls(floorPos,_tileMapVisualizer);
   }

   public HashSet<Vector2Int> RunRandomWalk(RandomWalkSO walkSO, Vector2Int position)
   {
      Vector2Int          curPos   = position;
      HashSet<Vector2Int> floorPos = new HashSet<Vector2Int>();

      for (int i = 0; i < walkSO.iterations; i++)
      {
         HashSet<Vector2Int> path = ProceduralRoomGen.RandomWalk(curPos,walkSO.walkLength);
         floorPos.UnionWith(path);

         if (walkSO.startRandomlyEachIteration)
            curPos = floorPos.ElementAt(Random.Range(0, floorPos.Count));
      }

      return floorPos;
   }
}
