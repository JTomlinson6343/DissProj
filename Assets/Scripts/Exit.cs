using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ExitType
{
    north,
    east,
    south,
    west
}

public class Exit : MonoBehaviour
{
    public ExitType m_ExitType;
    [HideInInspector] public Room m_RoomRef;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Perform exit behavior here
            Debug.Log("Player has collided with the exit!");

            switch (m_ExitType)
            {
                case ExitType.north:
                    m_RoomRef.GetComponentInParent<Floor>().MoveUp(m_RoomRef.m_Pos);
                    break;
                case ExitType.east:
                    m_RoomRef.GetComponentInParent<Floor>().MoveRight(m_RoomRef.m_Pos);
                    break;
                case ExitType.south:
                    m_RoomRef.GetComponentInParent<Floor>().MoveDown(m_RoomRef.m_Pos);
                    break;
                case ExitType.west:
                    m_RoomRef.GetComponentInParent<Floor>().MoveLeft(m_RoomRef.m_Pos);
                    break;
            }
        }
    }
}
