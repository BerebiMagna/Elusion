using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [Header("Player Settings")]
    public string name;

    [Header("Level 0 Room Prefabs")]
    public GameObject room1N;
    public GameObject room0;
    public GameObject room1;
    public GameObject room2;
    
    [Header("Misc Settings")]
    public GameObject playerPrefab;
    public Transform roomsParent;

    private RoomsList roomsList;
    private Player player;

    public void InitialiseRoomsList()
    {
        roomsList = new RoomsList(roomsParent);
    }

    public RoomsList GetRoomsList()
    {
        return roomsList;
    }

    public void SetRoomsList(RoomsList roomsList)
    {
        this.roomsList = roomsList;
    }

    public GameObject InstantiatePlayer()
    {
        return GameObject.Instantiate(playerPrefab, roomsList.GetSpawnRoom()., )
    }

    public void InitialisePlayer()
    {
        player = Player(name, )
    }

    private void Awake()
    {
        InitialiseRoomsList();
    }
}
