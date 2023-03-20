using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Floor
{
    // The 2D array which contains all the data about the current floor.
    public Room[,] m_MapArray;

    // The number of rooms for the floor
    int m_RoomLimit;

    // The number of rooms currently in the queue
    int m_RoomCount;

    // The size (width,height) of the floor
    int m_FloorWidth;
    int m_FloorHeight;

    // Queue of rooms to loop over
    Queue<Room> roomQueue = new Queue<Room>();

    // Number of neighbours allowed per cell
    const int m_Neighbourlimit = 2;

    // Direction lookup table to loop over when checking neighbours
    Vector2Int[] directionArray = {
            new Vector2Int(0, -1), new Vector2Int(1, 0),
            new Vector2Int(0, 1), new Vector2Int(-1, 0)
        };

    // Positionn of the starting room
    Vector2Int m_StartRoomPos;

    public Floor(int width, int height, int roomLimit)
    {
        m_RoomLimit = roomLimit;
        m_FloorWidth = width;
        m_FloorHeight = height;

        GenerateFloor();
    }

    void GenerateFloor()
    {
        m_StartRoomPos = new Vector2Int(m_FloorWidth / 2, m_FloorHeight / 2);

        // Instantiate starting room
        StartRoom startRoom = new StartRoom();

        // Init array to store rooms
        m_MapArray = new Room[m_FloorWidth, m_FloorHeight];

        // Add starting room to array
        InitRoom(m_StartRoomPos, startRoom);

        while (m_RoomCount < m_RoomLimit)
        {

            foreach (Room room in roomQueue)
            {
                foreach (Vector2Int dir in directionArray)
                {
                    // If a 50/50 chance happens, skip to the next possible room slot
                    if (Random.Range(0, 1) == 1)
                        continue;

                    Vector2Int pos = room.m_Pos + dir;

                    // Check if new cell is not already occupied
                    if (m_MapArray[pos.x, pos.y] != null)
                        continue;

                    // Check there arent already too many neighbours

                }
            }
        }
    }


    void InitRoom(Vector2Int pos, Room room)
    {
        // Place room in the position passed
        m_MapArray[pos.x, pos.y] = room;
        // Set position of room to position passed
        room.m_Pos = pos;
    }

    bool TooManyNeighboursCheck(Vector2Int pos, Room[,] mapArray)
    {
        int neighbourCount = 0;

        foreach (Vector2Int dir in directionArray)
        {
            // Check all neighbours of cell
            Vector2Int newPos = pos + dir;
            if (mapArray[newPos.x, newPos.y] != null)
            {
                // If a neighbouring cell is occupied, increment the counter
                neighbourCount++;
            }
        }

        // If the number of neighbours is too high, return true
        if (neighbourCount > m_Neighbourlimit)
        {
            return true;
        }
        return false;
    }
}