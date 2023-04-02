using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class Room: MonoBehaviour
{
    // Position of the room on the floor
    [HideInInspector] public Vector2Int m_Pos;
    // Bool for the generator to check when determining if a room has been added
    [HideInInspector] public bool m_RoomAdded = false;
    // Array of enemies for the room
    GameObject[] m_EnemyArray;

    // Array of spawn points of the room
    public Vector2[] m_SpawnPoints;
    // Array of enemy types that the room uses.
    public GameObject[] m_EnemyVariants;

    // Array of exits 
    public GameObject[] m_Exits;

    public Vector2 m_NorthExitPos;
    public Vector2 m_EastExitPos;
    public Vector2 m_SouthExitPos;
    public Vector2 m_WestExitPos;

    // Scene that will load when the room is entered.
    public SceneAsset m_Scene;

    virtual public void ChooseEnemySpawns()
    {
        // Return if no enemies are provided
        if (m_EnemyVariants.Length == 0 || m_EnemyVariants == null)
        {
            Debug.Log("Enemies not found.\n");
            return;
        }
        // Return if no spawnpoints are provided
        if (m_SpawnPoints.Length == 0 || m_SpawnPoints == null)
        {
            Debug.Log("Spawn points not found.\n");
            return;
        }

        // Set the number of enemies to the number of spawn points
        m_EnemyArray = new GameObject[m_SpawnPoints.Length];

        // Loop through each enemy in the enemy array
        for (int i = 0; i < m_EnemyArray.Length; i++)
        {
            // Set the enemy to a random one out of the number of enemies provided
            m_EnemyArray[i] = m_EnemyVariants[Random.Range(0, m_EnemyVariants.Length)];
            // Set the position of the enemy to the position defined in the spawn point
            m_EnemyArray[i].transform.position = m_SpawnPoints[i];
        }
    }
}