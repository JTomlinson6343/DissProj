using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomScene : MonoBehaviour
{
    // Width of the room
    public float m_Width;
    // Height of the room
    public float m_Height;

    BoxCollider2D m_Collider;

    // Position on the dungeon map
    public Vector2Int m_Pos;

    void Start()
    {
        Floor.RegisterRoom(this);

        InitCollider();
    }

    void InitCollider()
    {
        m_Collider = this.AddComponent<BoxCollider2D>();

        m_Collider.size = new Vector2(m_Width, m_Height);

        m_Collider.isTrigger = true;

        m_Collider.enabled = true;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CameraController.currentRoom = this;
        }
    }
}
