using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [Header("Room Properties")]
    public int floorIndex;
    public int roomIndex;

    public int GetFloorIndex()
    {
        return floorIndex;
    }

    public int GetRoomIndex()
    {
        return roomIndex;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetFloorIndex(int floorIndex)
    {
        this.floorIndex = floorIndex;
    }

    public void SetRoomIndex(int roomIndex)
    {
        this.roomIndex = roomIndex;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
}
