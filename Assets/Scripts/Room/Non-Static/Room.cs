using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//REWORK ROOM AS A SCRIPT TO ATTACH TO EACH ROOM WHICH WOULD REMOVE ALL THE METHODS IN ROOMMISC AND MAKE MAIN INITIALISATION OF ROOMLIST MUCH EASIER!!!!!!!!!
public class Room: MonoBehaviour
{
    private GameObject room;
    private Vector3 position;
    private int floorIndex;
    private int roomIndex;
    private bool spawn;

    public Room(GameObject room, int floorIndex, int roomIndex, bool spawn = false)
    {
        this.position = room.transform.position;
        this.floorIndex = floorIndex;
        this.roomIndex = roomIndex;
        this.spawn = spawn;
    }

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
        return position;
    }

    public bool GetSpawnBool()
    {
        return spawn;
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
        this.position = position;
    }

    public void SetSpawnBool(bool spawn)
    {
        this.spawn = spawn;
    }
}
