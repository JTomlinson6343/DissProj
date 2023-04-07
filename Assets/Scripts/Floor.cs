using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting.APIUpdating;
using static UnityEditor.PlayerSettings;

public class Floor : MonoBehaviour
{
    GameObject m_ExitPrefab;

    // The 2D array which contains all the data about the current floor.
    public Room[,] m_MapArray;

    // The number of rooms for the floor
    readonly int m_RoomLimit;

    // The size (width,height) of the floor
    readonly int m_MapDimensions;

    // Gameobject the room components are attached to
    GameObject m_ThisFloor;

    SceneAsset[] m_RoomVariants;

    public static Queue<Room> loadRoomQueue;
    // List of rooms to loop over
    List<Room> roomPosList;

    List<RoomObject> loadedRooms;

    // Number of neighbours allowed per cell
    readonly int m_Neighbourlimit = 2;

    // Direction lookup table to loop over when checking neighbours
    readonly Vector2Int[] directionArray = {
            new Vector2Int(0, -1), new Vector2Int(1, 0),
            new Vector2Int(0, 1), new Vector2Int(-1, 0)
        };

    public Floor(int dimensions, int roomLimit, int neighbourLimit, GameObject floor,
        SceneAsset[] roomVariants, Room[]  exitRoomVariants, Room[] startRoomVariants)
    {
        m_RoomLimit = roomLimit;
        m_MapDimensions = dimensions;
        m_Neighbourlimit = neighbourLimit;
        m_ThisFloor = floor;
        m_RoomVariants = roomVariants;

        GenerateFloor();
        if (loadRoomQueue == null)
            loadRoomQueue = new Queue<Room>();

        // Create the map in text form
        string mapstring = "";

        for (int x = 0; x < m_MapDimensions; x++)
        {
            for (int y = 0; y < m_MapDimensions; y++)
            {
                // Use [] to mean an occupied cell 
                if (m_MapArray[x, y] != null)
                {
                    mapstring += "[]";
                    loadRoomQueue.Enqueue(m_MapArray[x, y]);
                    LoadRoom(m_MapArray[x, y]);
                }
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
        Vector2Int m_StartRoomPos = new(m_MapDimensions / 2, m_MapDimensions / 2);

        // Init array to store rooms
        m_MapArray = new Room[m_MapDimensions, m_MapDimensions];

        roomPosList = new List<Room>();

        // Instantiate starting room
        GameObject roomObj = new GameObject();
        roomObj.transform.SetParent(m_ThisFloor.transform);
        Room startRoom = roomObj.AddComponent<Room>();

        roomObj.name = "TEST";

        //int roomChoice = Random.Range(0, m_RoomVariants.Length);

        //startRoom = m_RoomVariants[roomChoice].GetComponent<Room>();
        // Init the room
        InitRoom(m_StartRoomPos, startRoom);

        AddNeighbours();
    }

    void AddNeighbours()
    {
        // Counter to count how man times the algorithm fails to add a cell
        int failCount = 0;
        do
        {
            bool roomAdded = false;
            for (int i = 0; i < roomPosList.Count; i++)
            {
                // Loop through each cardinal direction and try and add a room to each one
                foreach (Vector2Int dir in directionArray)
                {
                    // If a 50/50 chance happens, skip to the next possible room slot
                    if (Random.Range(0, 11) >= 5) continue;

                    // Set current position to position of potential new room
                    Vector2Int newPos = roomPosList[i].m_Pos + dir;

                    // Check that the new room position isnt off the map
                    if (OutOfBoundsCheck(newPos)) continue;

                    // Check if new cell is not already occupied
                    if (m_MapArray[newPos.x, newPos.y] != null) continue;

                    // Check there arent already too many neighbours
                    if (TooManyNeighboursCheck(newPos)) continue;

                    InitRoom(newPos);

                    roomPosList[i].m_RoomAdded = true;
                    roomAdded = true;

                    // If the room limit has already been reached, stop trying to generate new ones
                    if (roomPosList.Count >= m_RoomLimit)
                        break;
                }
                // If the room limit has already been reached, stop trying to generate new ones
                if (roomPosList.Count >= m_RoomLimit)
                    break;
            }
            if (!roomAdded)
            {
                failCount++;
                // If there are no new additions 1000 times, stop the algorithm
                if (failCount >= 1000)
                {
                    Debug.Log("Could not add any more rooms. Only " + roomPosList.Count.ToString() + " could be added.");

                    break;
                }
            }

        } while (roomPosList.Count < m_RoomLimit);
    }

    void InitRoom(Vector2Int pos, Room room)
    {
        // Place room in the position passed
        m_MapArray[pos.x, pos.y] = room;
        // Set position of room to position passed
        room.m_Pos = pos;
        // Add room to room list
        roomPosList.Add(room);

    }
    void InitRoom(Vector2Int pos)
    {
        // Create object to contain the room
        GameObject roomObj = new GameObject();
        Room room = roomObj.AddComponent<Room>();

        // Pick a random room
        int roomChoice = Random.Range(0, m_RoomVariants.Length);
        roomObj.name = m_RoomVariants[roomChoice].name;

        // Set parent of the gameobject to the floor gameobject
        roomObj.transform.SetParent(m_ThisFloor.transform);

        InitRoom(pos, room);
    }

    void LoadRoom(Room room)
    {
        // Loads the scene by the name of the room passed in
        SceneManager.LoadScene(room.name, LoadSceneMode.Additive);

    }
    public static void RegisterRoom(RoomObject room)
    {
        // Return if all rooms are already loaded
        if (loadRoomQueue.Count == 0) return;

        // Remove room from queue and store in current room
        Room currentRoom = loadRoomQueue.Dequeue();

        // Set world position of the rooms relative to the other rooms in the dungeon
        room.transform.position = new Vector3(
            currentRoom.m_Pos.x * room.m_Width,
            currentRoom.m_Pos.y * room.m_Height, 0);

        // Set the name of the room to its relative position in the dungeon
        room.name = currentRoom.m_Pos.x.ToString() + "," + currentRoom.m_Pos.y.ToString();
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
        if (pos.x < 0 || pos.x > m_MapDimensions - 1)
            return true;

        // If y pos is off the map, return true
        if (pos.y < 0 || pos.y > m_MapDimensions-1)
            return true;

        return false;
    }
}
