/*
 * Source Code Originally by SunnyValleyStudio
 * Modified for usage by Ho-Sik Choo
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomDungeonGen : SimpleRandomWalkMapGen
{
    [SerializeField]                private int                               _minRoomWidth    = 4,  _minRoomHeight = 4;
    [SerializeField]                private int                               _dungeonWidth    = 20, _dungeonHeight = 20;
    [SerializeField] [Range(0, 10)] private int                               _offset          = 1;
    [SerializeField]                public  Vector2Int                        playerStartPos;

    [SerializeField] public Color[]                                     dungeonColors = new Color[4];
    public                  Dictionary<Vector2Int, HashSet<Vector2Int>> placeablePositions { get; set; } = new();
    public                  Dictionary<Vector2Int, HashSet<Vector2Int>> roamablePositions  { get; set; } = new();
    public                  HashSet<Vector2Int>                         allWallPositions;
    
    protected override void RunProceduralGen(ePlayerLocation dungeonNumber)
    {
        switch (dungeonNumber)
        {
            case ePlayerLocation.Dungeon0:
                Camera.main.backgroundColor = dungeonColors[0];
                break;
            case ePlayerLocation.Dungeon1:
                Camera.main.backgroundColor = dungeonColors[1];
                break;
            case ePlayerLocation.Dungeon2:
                Camera.main.backgroundColor = dungeonColors[2];
                break;
            case ePlayerLocation.Dungeon3:
                Camera.main.backgroundColor = dungeonColors[3];
                break;
            default:
                Debug.Log("Unknown Dungeon Number, Called from RoomDungeonGen.RunProceduralGen");
                break;
        }
        CreateRooms(dungeonNumber);
    }
    
    private void CreateRooms(ePlayerLocation dungeonNumber)
    {
        var roomList =
            ProceduralRoomGen
               .BinarySpacePartitioning(new BoundsInt((Vector3Int)_startPos, new Vector3Int(_dungeonWidth, _dungeonHeight,0)),_minRoomWidth,_minRoomHeight);

        List<Vector2Int> roomCentres = new List<Vector2Int>();
        foreach (var room in roomList)
        {
            roomCentres.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }
        playerStartPos = roomCentres[0];
        
        foreach (var centre in roomCentres)
        {
            placeablePositions.Add(centre, new HashSet<Vector2Int>());
        }
        
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        floor = CreateRoomsRandomly(roomList);
        
        HashSet<Vector2Int> corridors = ConnectRooms(roomCentres);
        floor.UnionWith(corridors);
        
        _tileMapVisualizer.PaintFloorTiles(floor, dungeonNumber);
        allWallPositions = WallGen.CreateWalls(floor,_tileMapVisualizer);
        
        EliminateFloorsNearWall();
        
        //Copy Room Positions for Monster AI 
        foreach (var position in placeablePositions)
        {
            roamablePositions.Add(position.Key, position.Value);
        }
    }

    private HashSet<Vector2Int> CreateRoomsRandomly(List<BoundsInt> roomList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        foreach (var roomBounds in roomList)
        {
            HashSet<Vector2Int> thisFloor = new HashSet<Vector2Int>();
            var                 roomCentre = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y));
            var                 roomFloor  = RunRandomWalk(_randomWalkSO, roomCentre);
            
            foreach (var position in roomFloor)
            {
                if(position.x >= (roomBounds.xMin + _offset) && position.x <= (roomBounds.xMax - _offset) && position.y >= (roomBounds.yMin - _offset) && position.y <= (roomBounds.yMax - _offset))
                {
                    floor.Add(position);
                    if (Random.value > 0.4f)
                        thisFloor.Add(position);
                }
            }
            
            placeablePositions[roomCentre].UnionWith(thisFloor);
        }

        return floor;
    }

    private void EliminateFloorsNearWall()
    {
        foreach (var room in placeablePositions)
        {
            for (int i = room.Value.Count - 1; i >= 0; i--)
            {
                HashSet<Vector2Int> floorsToEliminate = new HashSet<Vector2Int>();
                foreach (var dir in Direction2D.eightDirList)
                {
                    var                 curPos            = room.Value.ElementAt(i);
                    Vector2Int          neighborPos       = curPos + dir;
                    if (allWallPositions.Contains(neighborPos))
                    {
                        floorsToEliminate.Add(neighborPos);
                        floorsToEliminate.Add(curPos);
                    }
                }

                foreach (var target in floorsToEliminate)
                {
                    room.Value.Remove(target);
                }
            }
        }
        
    }
    

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCentres)
    {
        HashSet<Vector2Int> corridors         = new HashSet<Vector2Int>();
        var                 currentRoomCentre = playerStartPos;
        roomCentres.Remove(currentRoomCentre);

        while (roomCentres.Count > 0)
        {
            Vector2Int closest = FindClosestPointTo(currentRoomCentre, roomCentres);
            roomCentres.Remove(closest);
            HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCentre, closest);
            currentRoomCentre = closest;
            corridors.UnionWith(newCorridor);
        }

        return corridors;
    }

    private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCentre, Vector2Int destination)
    {
        HashSet<Vector2Int> corridor      = new HashSet<Vector2Int>();
        var                 position      = currentRoomCentre;
        corridor.Add(position);

        while (position.y != destination.y)
        {
            if (destination.y > position.y)
            {
                position += Vector2Int.up;
            }
            else if (destination.y < position.y)
            {
                position += Vector2Int.down;
            }
            
            corridor.Add(position);
        }

        while (position.x != destination.x)
        {
            if (destination.x > position.x)
            {
                position += Vector2Int.right;
            }
            else if (destination.x < position.x)
            {
                position += Vector2Int.left;
            }
            
            corridor.Add(position);
        }
        return corridor;
    }

    private Vector2Int FindClosestPointTo(Vector2Int currentRoomCentre, List<Vector2Int> roomCentres)
    {
        Vector2Int closest  = Vector2Int.zero;
        float      distance = float.MaxValue;
        foreach (var position in roomCentres)
        {
            float curDistance = Vector2.Distance(position, currentRoomCentre);
            if (curDistance < distance)
            {
                distance = curDistance;
                closest  = position;
            }
        }
        
        return closest;
    }
    
}
