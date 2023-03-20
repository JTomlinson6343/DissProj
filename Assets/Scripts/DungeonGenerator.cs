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

    // The size (width,height) of the floor
    [SerializeField] int m_FloorWidth;
    [SerializeField] int m_FloorHeight;

    // Array containing all possible rooms to be used in the dungeon
    [SerializeField] Room[] m_RoomVariants;

    // The number of rooms for the floor
    [SerializeField] int m_RoomLimit;

    // Array containing all the floors of a dungeon
    Floor[] m_Floors;

    // Start is called before the first frame update
    void Start()
    {
        // Starting room is placed at the centre of the floor

        for (int i = 0; i < m_FloorLimit;)
        {
            m_Floors[i] = new Floor(m_FloorWidth, m_FloorHeight, m_RoomLimit);
        }
        return;
    }
}