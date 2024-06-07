using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    private static GameManager         _instance = null;
    public static MapManager          SMapManager;
    public static GameObject          SPlayer;
    
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject GOMapManager;
    
    void Awake() {
        if (_instance == null) {
            _instance = this;
        }
        SPlayer              = player;
        SMapManager          = GOMapManager.GetComponent<MapManager>();
        
        SMapManager.Init();
        SMapManager.playerLocation = ePlayerLocation.Base;
    }

    void Start()
    {
        //TODO: Get Previous Location () 같은 함수로 저장된 상태를 불러오도록 해야함
        SMapManager.GenerateMapAndPlaceCharacter(ePlayerLocation.Dungeon0);
    }

    private void Update()
    {
        //Debug Codes
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SMapManager.IsCurrentFloorCleared = true;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            SMapManager.GenerateMapAndPlaceCharacter(ePlayerLocation.Base);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            //중요!!! Clear 반드시 해줘야 함 아니면 충돌 발생
            SMapManager.roomDungeonGen.placeablePositions.Clear();
            SMapManager.roomDungeonGen.allWallPositions.Clear();
            SMapManager.GenerateMapAndPlaceCharacter(ePlayerLocation.Dungeon2);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            SMapManager.GenerateMapAndPlaceCharacter(ePlayerLocation.Boss2);
        }
        // Debug Codes end

        if (SMapManager.playerLocation == ePlayerLocation.Dungeon0 
            || SMapManager.playerLocation == ePlayerLocation.Dungeon1
            || SMapManager.playerLocation == ePlayerLocation.Dungeon2
            ||SMapManager.playerLocation == ePlayerLocation.Dungeon3  )
        {
            if (SMapManager.IsCurrentFloorCleared)
            {
                if (SMapManager.CurrentDungeonFloorCount == 6)
                {
                    SMapManager.roomDungeonGen.placeablePositions.Clear();
                    SMapManager.roomDungeonGen.roamablePositions.Clear();
                    SMapManager.roomDungeonGen.allWallPositions.Clear();
                    SMapManager.CurrentDungeonFloorCount = 0;
                    switch (SMapManager.playerLocation)
                    {
                        case ePlayerLocation.Dungeon0:
                            SMapManager.GenerateMapAndPlaceCharacter(ePlayerLocation.Boss1);
                            break;
                        case ePlayerLocation.Dungeon1:
                            SMapManager.GenerateMapAndPlaceCharacter(ePlayerLocation.Boss2);
                            break;
                        case ePlayerLocation.Dungeon2:
                            SMapManager.GenerateMapAndPlaceCharacter(ePlayerLocation.Boss3);
                            break;
                        case ePlayerLocation.Dungeon3:
                            SMapManager.GenerateMapAndPlaceCharacter(ePlayerLocation.Boss4);
                            break;
                        default:
                            Debug.Log("Wrong location called from GameManager Update");
                            break;
                    }
                }
                else
                { 
                    SMapManager.roomDungeonGen.placeablePositions.Clear();
                    SMapManager.roomDungeonGen.roamablePositions.Clear();
                    SMapManager.roomDungeonGen.allWallPositions.Clear();
                    SMapManager.GenerateMapAndPlaceCharacter(SMapManager.playerLocation);
                }
            }
        }
    }
}
