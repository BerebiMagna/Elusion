using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private string name;
    private GameObject playerObj;

    public Player(string name, GameObject playerObj)
    {
        this.name = name;
        this.playerObj = playerObj;
    }

    public string GetName()
    {
        return name;
    }

    public GameObject GetPlayerObject()
    {
        return playerObj;
    }

    public Vector3 GetPlayerPosition()
    {
        return playerObj.transform.position;
    }

    public Vector3 GetPlayerRoomPosition()
    {
        return playerObj.transform.parent.position;
    }

    public Room GetPlayerRoom(RoomsList roomList)
    {
        return roomList.GetRoomByPosition(GetPlayerRoomPosition());
    }
}
