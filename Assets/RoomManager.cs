using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : Singleton<RoomManager>
{
    public GameObject room1, room2, room3, room4;
    public float correctRoom = 1;
    public bool isInCorrectRoom;
}
