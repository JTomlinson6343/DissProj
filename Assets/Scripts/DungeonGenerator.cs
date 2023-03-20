using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class DungeonGenerator : MonoBehaviour
{
    // The number of floors generated for the dungeon
    [SerializeField] int    m_FloorLimit;
    // The number of rooms for the floor
    [SerializeField] int    m_RoomLimit;

    // Array containing all possible rooms to be used in the dungeon
    [SerializeField] Room[] m_RoomVariants;

    // The number of rooms currently in the queue
    int m_RoomCount;

    // Array containing all the floors of a dungeon
    Floor[] m_Floors;

    // The size (width,height) of the floor
    [SerializeField] int m_FloorWidth;
    [SerializeField] int m_FloorHeight;

    // Positionn of the starting room
    Vector2Int m_StartRoomPos;

    // Start is called before the first frame update
    void Start()
    {
        // Starting room is placed at the centre of the floor
        m_StartRoomPos = new Vector2Int(m_FloorWidth / 2, m_FloorHeight / 2);

        GenerateFloor();
        return;
    }

    void GenerateFloor()
    {

        // Make queue of rooms
        Queue<Room> roomQueue = new Queue<Room>();

        // Instantiate starting room
        StartRoom startRoom = new StartRoom();

        // Instantiate floor
        Floor floor = new Floor();

        // Init array to store rooms
        Room[,] mapArray = new Room[m_FloorWidth, m_FloorHeight];

        // Add starting room to array
        InitRoom(m_StartRoomPos, mapArray, startRoom);

        Vector2Int[] directionArray = {
            new Vector2Int(0, -1), new Vector2Int(1, 0),
            new Vector2Int(0, 1), new Vector2Int(-1, 0) 
        };

        while (m_RoomCount < m_RoomLimit)
        {
            
            foreach (Room room in roomQueue)
            {
                foreach(Vector2Int dir in directionArray)
                {
                    // If a 50/50 chance happens, skip to the next possible room slot
                    if (Random.Range(0, 1) == 1)
                        continue;

                    Vector2Int pos = room.m_Pos + dir;

                    // Check if new cell is not already occupied
                    if (mapArray[pos.x, pos.y] != null)
                        continue;

                    // Check there arent already too many neighbours

                }
            }
        }
    }


    void InitRoom(Vector2Int pos, Room[,] mapArray, Room room)
    {
        // Place room in the position passed
        mapArray[pos.x,pos.y] = room;
        // Set position of room to position passed
        room.m_Pos = pos;
    }
}