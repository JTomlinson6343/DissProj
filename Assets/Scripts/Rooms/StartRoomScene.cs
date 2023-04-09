using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRoomScene : RoomScene
{
    private void Start()
    {
        RoomScene
        CameraController.currentRoom = this;
    }
}
