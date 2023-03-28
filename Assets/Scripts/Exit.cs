using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ExitType
{
    north,
    east,
    south,
    west
}

public class Exit : MonoBehaviour
{
    ExitType m_ExitType;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Perform exit behavior here
            Debug.Log("Player has collided with the exit!");
        }
    }
}
