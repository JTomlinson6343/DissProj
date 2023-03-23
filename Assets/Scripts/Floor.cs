using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Floor
{
    // The 2D array which contains all the data about the current floor.
    public Room[,] m_MapArray;

    // The number of rooms for the floor
    readonly int m_RoomLimit;

    // The size (width,height) of the floor
    readonly int m_FloorWidth;
    readonly int m_FloorHeight;

    // List of rooms to loop over
    List<Room> roomPosList;

    // Number of neighbours allowed per cell
    readonly int m_Neighbourlimit = 2;

    // Direction lookup table to loop over when checking neighbours
    readonly Vector2Int[] directionArray = {
            new Vector2Int(0, -1), new Vector2Int(1, 0),
            new Vector2Int(0, 1), new Vector2Int(-1, 0)
        };

    public Floor(int width, int height, int roomLimit, int neighbourLimit)
    {
        m_RoomLimit = roomLimit;
        m_FloorWidth = width;
        m_FloorHeight = height;
        m_Neighbourlimit = neighbourLimit;

        GenerateFloor();

        // Create the map in text form
        string mapstring = "";

        for (int x = 0; x < m_FloorWidth; x++)
        {
            for (int y = 0; y < m_FloorHeight; y++)
            {
                // Use [] to mean an occupied cell 
                if (m_MapArray[x, y] != null)
                    mapstring += "[]";
                // Use # to mean an unoccupied cell
                else
                    mapstring += "#";
            }
            // Add a new line for each new row of cells
            mapstring += '\n';
        }
        // Print text map to console
        Debug.Log(mapstring);
    }

    void GenerateFloor()
    {
        // Init starting room position to half 
        Vector2Int m_StartRoomPos = new Vector2Int(m_FloorWidth / 2, m_FloorHeight / 2);

        // Init array to store rooms
        m_MapArray = new Room[m_FloorWidth, m_FloorHeight];

        roomPosList = new List<Room>();

        // Instantiate starting room
        GameObject obj = new GameObject();
        StartRoom startRoom = obj.AddComponent<StartRoom>();

        // Add starting room to array
        InitRoom(m_StartRoomPos, startRoom);

        // Add the room to the list
        roomPosList.Add(startRoom);

        AddNeighbours();
    }

    void AddNeighbours()
    {
        for (int i = 0; i < roomPosList.Count; i++)
        {
            // Loop through each cardinal direction and try and add a room to each one
            foreach (Vector2Int dir in directionArray)
            {
                // If a 50/50 chance happens, skip to the next possible room slot
                if (Random.Range(0, 2) == 1) continue;

                // Set current position to position of potential new room
                Vector2Int newPos = roomPosList[i].m_Pos + dir;

                // Check that the new room position isnt off the map
                if (OutOfBoundsCheck(newPos)) continue;

                // Check if new cell is not already occupied
                if (m_MapArray[newPos.x, newPos.y] != null) continue;

                // Check there arent already too many neighbours
                if (TooManyNeighboursCheck(newPos)) continue;

                GameObject obj = new GameObject();
                Room newRoom = obj.AddComponent<Room>();
                InitRoom(newPos, newRoom);

                roomPosList.Add(newRoom);
                roomPosList[i].m_RoomAdded = true;


                if (roomPosList.Count >= m_RoomLimit)
                    break;
            }

            if (roomPosList.Count >= m_RoomLimit)
                break;
        }

        // If the room limit is not yet reached, loop through all the rooms in the list again
        if (roomPosList.Count < m_RoomLimit)
            AddNeighbours();
    }

    void InitRoom(Vector2Int pos, Room room)
    {
        // Place room in the position passed
        m_MapArray[pos.x, pos.y] = room;
        // Set position of room to position passed
        room.m_Pos = pos;
    }

    bool TooManyNeighboursCheck(Vector2Int pos)
    {
        int neighbourCount = 0;

        foreach (Vector2Int dir in directionArray)
        {
            // Check all neighbours of cell
            Vector2Int newPos = pos + dir;

            if (OutOfBoundsCheck(newPos)) continue;

            if (m_MapArray[newPos.x, newPos.y] != null)
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

    bool OutOfBoundsCheck(Vector2Int pos)
    {
        // If x pos is off the map, return true
        if (pos.x < 0 || pos.x > m_FloorWidth-1)
            return true;

        // If y pos is off the map, return true
        if (pos.y < 0 || pos.y > m_FloorHeight-1)
            return true;

        return false;
    }
}