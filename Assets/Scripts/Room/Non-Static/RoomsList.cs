using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsList
{
    private Room[] roomArray;

    public RoomsList(Transform roomsParent, Dictionary<int, GameObject> roomPrefabs)
    {
        roomArray = GetRoomArray(roomsParent, roomPrefabs);
    }

    public Room[] GetRoomArray(Transform roomsParent, Dictionary<int, GameObject> roomPrefabs)
    {
        return new Room[]
        {
            new Room(GameObject.Instantiate(roomPrefabs[0], new Vector3(0, 0, 0), Quaternion.identity, roomsParent), 0, 0, spawn: true),
            new Room(GameObject.Instantiate(roomPrefabs[0], new Vector3(10, 0, 0), Quaternion.identity, roomsParent), 0, 0),
            new Room(GameObject.Instantiate(roomPrefabs[0], new Vector3(20, 0, 0), Quaternion.identity, roomsParent), 0, 0),
            new Room(GameObject.Instantiate(roomPrefabs[0], new Vector3(30, 0, 0), Quaternion.identity, roomsParent), 0, 0),
            new Room(GameObject.Instantiate(roomPrefabs[0], new Vector3(40, 0, 0), Quaternion.identity, roomsParent), 0, 0),
            new Room(GameObject.Instantiate(roomPrefabs[0], new Vector3(0, 0, 10), Quaternion.identity, roomsParent), 0, 0),
            new Room(GameObject.Instantiate(roomPrefabs[0], new Vector3(10, 0, 10), Quaternion.identity, roomsParent), 0, 0),
            new Room(GameObject.Instantiate(roomPrefabs[0], new Vector3(20, 0, 10), Quaternion.identity, roomsParent), 0, 0),
            new Room(GameObject.Instantiate(roomPrefabs[0], new Vector3(30, 0, 10), Quaternion.identity, roomsParent), 0, 0),
            new Room(GameObject.Instantiate(roomPrefabs[0], new Vector3(40, 0, 10), Quaternion.identity, roomsParent), 0, 0),
            new Room(GameObject.Instantiate(roomPrefabs[0], new Vector3(0, 0, 20), Quaternion.identity, roomsParent), 0, 0),
            new Room(GameObject.Instantiate(roomPrefabs[0], new Vector3(10, 0, 20), Quaternion.identity, roomsParent), 0, 0),
            new Room(GameObject.Instantiate(roomPrefabs[0], new Vector3(20, 0, 20), Quaternion.identity, roomsParent), 0, 0),
            new Room(GameObject.Instantiate(roomPrefabs[0], new Vector3(30, 0, 20), Quaternion.identity, roomsParent), 0, 0),
            new Room(GameObject.Instantiate(roomPrefabs[0], new Vector3(40, 0, 20), Quaternion.identity, roomsParent), 0, 0),
            new Room(GameObject.Instantiate(roomPrefabs[0], new Vector3(0, 0, 30), Quaternion.identity, roomsParent), 0, 0),
            new Room(GameObject.Instantiate(roomPrefabs[0], new Vector3(10, 0, 30), Quaternion.identity, roomsParent), 0, 0),
            new Room(GameObject.Instantiate(roomPrefabs[0], new Vector3(20, 0, 30), Quaternion.identity, roomsParent), 0, 0),
            new Room(GameObject.Instantiate(roomPrefabs[0], new Vector3(30, 0, 30), Quaternion.identity, roomsParent), 0, 0),
            new Room(GameObject.Instantiate(roomPrefabs[0], new Vector3(40, 0, 30), Quaternion.identity, roomsParent), 0, 0),
            new Room(GameObject.Instantiate(roomPrefabs[0], new Vector3(0, 0, 40), Quaternion.identity, roomsParent), 0, 0),
            new Room(GameObject.Instantiate(roomPrefabs[0], new Vector3(10, 0, 40), Quaternion.identity, roomsParent), 0, 0),
            new Room(GameObject.Instantiate(roomPrefabs[0], new Vector3(20, 0, 40), Quaternion.identity, roomsParent), 0, 0),
            new Room(GameObject.Instantiate(roomPrefabs[0], new Vector3(30, 0, 40), Quaternion.identity, roomsParent), 0, 0),
            new Room(GameObject.Instantiate(roomPrefabs[0], new Vector3(40, 0, 40), Quaternion.identity, roomsParent), 0, 0)
        };
    }

    public Room GetRoomByPosition(Vector3 position)
    {
        foreach (Room r in roomArray)
        {
            if (r.GetPosition().Equals(position))
            {
                return r;
            }
        }
        return null;
    }

    public Room GetSpawnRoom()
    {
        foreach (Room r in roomArray)
        {
            if (r.GetSpawnBool())
            {
                return r;
            }
        }
        return null;
    }
}
