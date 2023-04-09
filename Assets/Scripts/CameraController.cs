using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static RoomScene currentRoom;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCamera();
    }

    void UpdateCamera()
    {

    }

    Vector3 GetNewPosition()
    {
        if (currentRoom == null)
        {
            return Vector3.zero;
        }

        return new Vector3(currentRoom.GetRoomCenterPosition(),;
    }
}
