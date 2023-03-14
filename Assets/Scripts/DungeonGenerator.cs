using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class DungeonGenerator : MonoBehaviour
{
    // The number of floors generated for the dungeon
    [SerializeField] int  m_FloorLimit;
    // The number of rooms for the floor
    [SerializeField] int  m_RoomLimit;
    // The number of rooms currently in the queue
    [SerializeField] int  m_RoomCount;

    // Array containing all the floors of a dungeon
    SceneAsset[] m_Floors;

    // Array containing all possible rooms to be used in the dungeon
    [SerializeField] Room[]     m_RoomVariants;

    // Start is called before the first frame update
    void Start()
    {
        return;
    }
}