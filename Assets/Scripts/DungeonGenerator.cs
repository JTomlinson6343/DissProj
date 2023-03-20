using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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
    Vector2 m_StartRoomPos;

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
        Queue<Room> roomQueue = new Queue<Room>();

        StartRoom startRoom = new StartRoom();

        Floor floor = new Floor();
        Room[,] mapArray = new Room[m_FloorWidth, m_FloorHeight];



        Debug.Log(mapArray[m_FloorWidth/2, m_FloorHeight/2].m_Pos);
    }

    void InitRoom(Vector2Int pos, Room[,] mapArray, Room room)
    {
        // Place room in the position passed
        mapArray[pos.x,pos.y] = room;
        // Set position of room to position passed
        room.m_Pos = pos;
    }
}