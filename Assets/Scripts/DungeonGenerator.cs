using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    // The number of floors generated for the dungeon
    [SerializeField] int m_FloorLimit;

    // The size (width,height) of the floor
    int m_FloorDimensions = 10;

    // The number of rooms for the floor
    [SerializeField] int m_RoomLimit;

    // Number of neighbours a new room is allowed to have before it is added
    [SerializeField] int m_Neighbourlimit = 1;

    // Array containing all possible rooms to be used in the dungeon
    [SerializeField] Room[] m_RoomVariants;
    [SerializeField] StartRoom[] m_StartRoomVariants;
    [SerializeField] ExitRoom[] m_ExitRoomVariants;

    // Array containing all the floors of a dungeon
    Floor[] m_Floors;

    // Start is called before the first frame update
    void Start()
    {
        int dimValue = (int)math.sqrt(m_RoomLimit) * 2;
        m_FloorDimensions = dimValue;

        m_Floors = new Floor[m_FloorLimit];

        for (int i = 0; i < m_FloorLimit; i++)
        {
            // Create an empty gameobject to contain all rooms in a floor
            GameObject floor = new GameObject();
            floor.name = "Floor " + i.ToString();

            // Set parent of the floor to the dungeon generator object
            floor.transform.SetParent(transform);

            // Create a floor object
            m_Floors[i] = new Floor(m_FloorDimensions, m_RoomLimit, m_Neighbourlimit, floor,
                m_RoomVariants, m_ExitRoomVariants, m_StartRoomVariants);
        }
    }
}