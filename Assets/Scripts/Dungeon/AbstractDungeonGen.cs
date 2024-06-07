using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDungeonGen : MonoBehaviour
{
   [SerializeField] protected TileMapVisualizer _tileMapVisualizer;
   [SerializeField] protected Vector2Int _startPos = Vector2Int.zero;

   public void GenerateDungeon(ePlayerLocation dungeonNumber)
   {
      _tileMapVisualizer.Clear();
      RunProceduralGen(dungeonNumber);
   }

   protected abstract void RunProceduralGen(ePlayerLocation dungeonNumber);
}
