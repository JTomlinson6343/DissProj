using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static RoomScene currentRoom;

    [SerializeField] float m_CameraMoveSpeed;

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
        Vector3 newRoomPos = GetNewPosition();

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

        Vector3 newPos = currentRoom.GetRoomCenterPosition();

        return new Vector3(newPos.x,newPos.y,transform.position.z);
    }
}
