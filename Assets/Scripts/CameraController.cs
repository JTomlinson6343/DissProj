using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static RoomScene currentRoom;

    [SerializeField] float m_CameraMoveSpeed;

    // Update is called once per frame
    void Update()
    {
        UpdateCamera();
    }

    void UpdateCamera()
    {
        Vector3 newRoomPos = GetNewPosition();

        // Move the camera towards the position of the new room
        transform.position = Vector3.MoveTowards(
            transform.position,
            newRoomPos,
            Time.deltaTime * m_CameraMoveSpeed);
    }

    Vector3 GetNewPosition()
    {
        if (currentRoom == null)
        {
            return Vector3.zero;
        }
        // Get position of the center of the room
        Vector3 newPos = currentRoom.GetRoomCenterPosition();

        // Return x and y positions of the room and keep z the same
        return new Vector3(newPos.x,newPos.y,transform.position.z);
    }
}
