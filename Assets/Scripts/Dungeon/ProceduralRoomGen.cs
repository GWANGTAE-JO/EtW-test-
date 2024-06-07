/*
 * Source Code Originally by SunnyValleyStudio
 * Modified for usage by Ho-Sik Choo
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public static class ProceduralRoomGen 
{
    public static HashSet<Vector2Int> RandomWalk(Vector2Int startPos, int walkLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        path.Add(startPos);
        Vector2Int prevPos = startPos;
        for (int i = 0; i < walkLength; i++) 
        {
            Vector2Int newPos = prevPos + Direction2D.GetRandomDirection();
            path.Add(newPos);
            prevPos = newPos;
        }

        return path;
    }

    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPos, int corridorLength)
    {
        List<Vector2Int> retCorridorList = new List<Vector2Int>();

        Vector2Int dir    = Direction2D.GetRandomDirection();
        Vector2Int curPos = startPos;
        retCorridorList.Add(curPos);

        for (int i = 0; i < corridorLength; i++)
        {
            curPos += dir;
            retCorridorList.Add(curPos);
        }

        return retCorridorList;
    }

    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSplit, int minWidth, int minHeight)
    {
        Queue<BoundsInt> roomsQueue   = new Queue<BoundsInt>();
        List<BoundsInt>  retRoomsList = new List<BoundsInt>();
        
        roomsQueue.Enqueue(spaceToSplit);
        
        while (roomsQueue.Count > 0)
        {
            BoundsInt room = roomsQueue.Dequeue();
            if (room.size.x >= minWidth && room.size.y >= minHeight)
            {
                if (Random.value > 0.5f)
                {
                    if (room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minWidth, roomsQueue, room);
                    }
                    else if (room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minHeight, roomsQueue, room);
                    }
                    else
                    {
                        retRoomsList.Add(room);
                    }
                }
                else
                {
                    if (room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minHeight, roomsQueue, room);
                    }
                    else if (room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minWidth, roomsQueue, room);
                    }
                    else
                    {
                        retRoomsList.Add(room);
                    }
                }
            }
        }

        return retRoomsList;
    }

    private static void SplitVertically(int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var       xSplit = Random.Range(1, room.size.x);
        BoundsInt room1  = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x  + xSplit, room.min.y,  room.min.z),
                                        new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }

    private static void SplitHorizontally(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var       ySplit = Random.Range(1, room.size.y);
        BoundsInt room1  = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit,  room.min.z),
                                        new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }
}

public static class Direction2D
{
    public static List<Vector2Int> dirList = new List<Vector2Int>
                                             {
                                                 new Vector2Int(0,  1),  //UP
                                                 new Vector2Int(1,  0),  //RIGHT
                                                 new Vector2Int(0,  -1), // DOWN
                                                 new Vector2Int(-1, 0)   //LEFT
                                             };

    public static List<Vector2Int> diagonalDirList = new List<Vector2Int>
                                                     {
                                                         new Vector2Int(1,  1),  //UP-RIGHT
                                                         new Vector2Int(1,  -1), //RIGHT-DOWN
                                                         new Vector2Int(-1, -1), // DOWN-LEFT
                                                         new Vector2Int(-1, 1)   //LEFT-UP
                                                     };
    
    public static List<Vector2Int> eightDirList = new List<Vector2Int>
                                                         {
                                                             new Vector2Int(0,  1),  
                                                             new Vector2Int(1,  1),  
                                                             new Vector2Int(1,  0),  
                                                             new Vector2Int(1,  -1), 
                                                             new Vector2Int(0,  -1), 
                                                             new Vector2Int(-1, -1), 
                                                             new Vector2Int(-1, 0),  
                                                             new Vector2Int(-1, 1)   

                                                         };

    public static Vector2Int GetRandomDirection()
    {
        return dirList[Random.Range(0, dirList.Count)];
    }
}
