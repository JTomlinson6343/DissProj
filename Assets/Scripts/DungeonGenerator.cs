using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    // The number of floors generated for the dungeon
    [SerializeField] const int  m_FloorLimit = 1;
    // The number of rooms for the floor
    [SerializeField] const int  m_RoomLimit = 1;
    // The number of rooms currently in the queue
    [SerializeField] const int  m_RoomCount = 0;
    // Array containing all the floors of a dungeon.
    Floor[]                     m_Floors;

    // Start is called before the first frame update
    void Start()
    {
        
    }
}