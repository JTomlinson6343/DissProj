using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitRoom : Room
{
    [SerializeField] Vector2 m_ExitPos;         // Position the exit spawns at
    [SerializeField] GameObject m_ExitPrefab;   // Prefab of the exit object that teleports the player to the next floor

    // Start is called before the first frame update
    void Start()
    {
        // Creates an isntance of the exit prefab
        Instantiate(m_ExitPrefab);
        // Sets the position of the exit to the position set by the room
        m_ExitPrefab.transform.position = m_ExitPos;
        // Hide the exit until SpawnRoom is called
        m_ExitPrefab.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Called when creating the room on the floor layout
    public void SpawnRoom()
    {
        // Reveal exit
        m_ExitPrefab.SetActive (true);
    }
}
