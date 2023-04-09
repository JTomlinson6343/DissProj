using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomObject : MonoBehaviour
{
    // Width of the room
    public float m_Width;
    // Height of the room
    public float m_Height;

    // Position on the dungeon map
    public Vector2Int m_Pos;

    bool m_Loaded = false;

    void Start()
    {
        Floor.RegisterRoom(this);

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(m_Width, m_Height, 0));
    }

    public Vector3 GetRoomCenterPosition()
    {
        return new Vector3(m_Width/2, m_Height/2, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
