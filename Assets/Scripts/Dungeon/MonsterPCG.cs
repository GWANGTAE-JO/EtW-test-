using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterPcg : MonoBehaviour
{
   [SerializeField] public RoomDungeonGen   roomDungeonGen;
   [SerializeField] public List<GameObject> placeableMonsters;
   [SerializeField] public int              maxMonsterPerRoom;
   [SerializeField] public int              minMonsterPerRoom;
   [SerializeField] public GameObject       monsterHolder;
   
   public Dictionary<Vector2Int, HashSet<Vector2Int>> roomAndFloorsToPlaceMonsters;
   
   public void ProceduralGenMonsters()
   {
      roomAndFloorsToPlaceMonsters = roomDungeonGen.placeablePositions;
      PlaceMonsters();
   }

   private void PlaceMonsters()
   {
      foreach (var room in roomAndFloorsToPlaceMonsters)
      {
         List<Vector2Int> positionList     = room.Value.ToList();
         List<GameObject> selectedMonsters = SelectMonsters(Random.Range(minMonsterPerRoom, maxMonsterPerRoom));

         foreach (var monster in selectedMonsters)
         {
            //Debug.Log(positionList.Count);
            if (positionList.Count <= 0)
            {
               continue;
            }
            
            Vector2Int position = positionList[Random.Range(0, positionList.Count - 1)];
            
            //몬스터 주위 8방향 픽셀 위치도 제거할 것 (몬스터 픽셀 크기 고려)
            foreach (Vector2Int eightDir in Direction2D.eightDirList)
            {
               Vector2Int enemyNearPos = eightDir + position;
               if (positionList.Contains(enemyNearPos))
               {
                  positionList.Remove(enemyNearPos);
               }
            }
            positionList.Remove(position);
            
            // Placing function
            GameObject monsterPrefab = Instantiate(monster,monsterHolder.transform);
            monsterPrefab.transform.position = new Vector3(position.x, position.y, 0);
         }
      }
   }

   private List<GameObject> SelectMonsters(int monsterCount)
   {
      List<GameObject> retSelectedMonsters = new List<GameObject>();

      for (int i = 0; i < monsterCount; i++)
      {
         GameObject monster = placeableMonsters[Random.Range(0, placeableMonsters.Count - 1)];
         retSelectedMonsters.Add(monster);
      }
      
      return retSelectedMonsters;
   }

   public void DestroyExistingEnemy()
   {
      foreach (Transform child in monsterHolder.transform)
      {
         Destroy(child.gameObject);
      }
   }
}
