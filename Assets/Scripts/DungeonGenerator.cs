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
    [SerializeField] int m_FloorWidth;
    [SerializeField] int m_FloorHeight;

    // Array containing all possible rooms to be used in the dungeon
    [SerializeField] Room[] m_RoomVariants;

    // The number of rooms for the floor
    [SerializeField] int m_RoomLimit;

    [SerializeField] int m_Neighbourlimit = 2;

    // Array containing all the floors of a dungeon
    Floor[] m_Floors;

    // Start is called before the first frame update
    void Start()
    {
        m_Floors = new Floor[m_FloorLimit];

        // Check to make sure there is enough space for all the rooms
        if (m_RoomLimit <= m_FloorWidth * m_FloorHeight)
        {
            for (int i = 0; i < m_FloorLimit; i++)
            {
                // Create an empty gameobject to contain all rooms in a floor
                GameObject floor = new GameObject();
                floor.name = "Floor " + i.ToString();

                // Set parent of the floor to the dungeon generator object
                floor.transform.SetParent(transform.parent);

                // Create a floor object
                m_Floors[i] = new Floor(m_FloorWidth, m_FloorHeight, m_RoomLimit, m_Neighbourlimit, floor);
            }
        }
        else
        {
            Debug.Log("ERROR GENERATING MAP: Not enough space for the number of rooms specified. Decrease room limit or increase map width and/or height.");
        }
    }
}