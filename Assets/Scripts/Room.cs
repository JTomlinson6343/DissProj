using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room
{
    public Vector2 m_Pos;
    public bool m_RoomAdded = false;
    [SerializeField] GameObject[] m_EnemyArray;
    [SerializeField] Vector2[] m_SpawnPoints;
    [SerializeField] Tilemap m_Tilemap;
    [SerializeField] SceneAsset m_Scene;
}

public class Floor
{
    // The 2D array which contains all the data about the current floor.
    Room[][] m_MapArray;
}