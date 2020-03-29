using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceRooms : MonoBehaviour
{
    [Header("Place Rooms Properties")]
    public int size = 100;
    public int brushIndex = 0;

    [Header("Place Rooms Misc")]
    public Camera cam;
    public GameObject basePrefab;
    public EditorCameraMovement editorCamMovScript;
    public EditorMenus editorMenusScript;

    [Header("Floor 0")]
    [Header("Place Rooms' Holograms")]
    public GameObject h_f0R1n;
    public GameObject h_f0R0;
    public GameObject h_f0R1;
    public GameObject h_f0R2;

    [Header("Floor 0")]
    [Header("Place Rooms' Room Prefabs")]
    public GameObject f0R1n;
    public GameObject f0R0;
    public GameObject f0R1;
    public GameObject f0R2;

    private GameObject roomParent;
    private GameObject[] gridArray;
    private List<GameObject> roomList;
    private List<Vector3> roomPosList;
    private List<GameObject> hologramRoomList;
    private List<Vector3> hologramRoomPosList;
    private Dictionary<int, GameObject>[] roomPrefabsArray;
    private Dictionary<int, GameObject>[] roomPrefabsHologramArray;
    private KeyValuePair<int, int> floorRoomIndex;


    public void ChangeBrushIndex(int index)
    {
        brushIndex = index;
    }

    private GameObject GetTileSingular()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.transform.gameObject;
        }
        return null;
    }

    private Vector3 GetTileInBoundaries(int z, int x, Transform tile)
    {
        Vector3 position = new Vector3(tile.position.x + x, tile.position.y, tile.position.z + z);
        if(position.x < size / 2 * 10 && position.z < size / 2 * 10 && position.x >= -size / 2 * 10 && position.z >= -size / 2 * 10)
        {
            return position;
        }
        return new Vector3(-1, -1, -1); //arbitary null value
    }

    private void PlaceRoom(Vector3 position)
    {
        if (position.x != -1)
        {
            if (!roomPosList.Contains(position))
            {
                GameObject room = GameObject.Instantiate(roomPrefabsArray[floorRoomIndex.Key][floorRoomIndex.Value], position, Quaternion.identity, roomParent.transform);
                room.name = string.Format("({0}, {1}, {2})", position.x, position.y, position.z);
                roomList.Add(room);
                roomPosList.Add(room.transform.position);
                string gridName = string.Format("Tile ({0}, {1})", (int)position.z / 10, (int)position.x / 10);
                foreach (GameObject gObj in gridArray)
                {
                    if (gObj.name == gridName)
                    {
                        gObj.GetComponent<MeshRenderer>().enabled = false;
                        break;
                    }
                }
            }
        }
    }

    private void PlaceRoomSquare(int sizeX, int sizeZ)
    {
        if(sizeX % 2 != 0 && sizeZ % 2 != 0)
        {
            if (Input.GetMouseButton(0))
            {
                if (!Miscellaneous.IsPointerOverUIElement())
                {
                    GameObject tile = GetTileSingular();
                    if (tile != null)
                    {
                        int xLimit = Mathf.FloorToInt(sizeX / 2) * 10;
                        int zLimit = Mathf.FloorToInt(sizeZ / 2) * 10;
                        for (int x = -xLimit; x <= xLimit; x += 10)
                        {
                            for (int z = -zLimit; z <= zLimit; z += 10)
                            {
                                Vector3 position = GetTileInBoundaries(z, x, tile.transform);
                                PlaceRoom(position);
                            }
                        }
                    }
                }
            }
        }
    }

    private void PlaceRoomStar(int radius)
    {
        if (Input.GetMouseButton(0))
        {
            if (!Miscellaneous.IsPointerOverUIElement())
            {
                GameObject tile = GetTileSingular();
                if (tile != null)
                {
                    int xLimit = radius * 10;
                    int zLimit = 0;
                    for (int x = -xLimit; x <= xLimit + 20; x += 10)
                    {
                        for (int z = -zLimit; z <= zLimit; z += 10)
                        {
                            Vector3 position = GetTileInBoundaries(z, x, tile.transform);
                            PlaceRoom(position);
                        }
                        if (x >= 0)
                        {
                            zLimit -= 10;
                        }
                        else
                        {
                            zLimit += 10;
                        }
                    }
                }
            }
        }
    }

    private void RemoveRoom(Vector3 position)
    {
        if (position.x != -1)
        {
            if (roomPosList.Contains(position))
            {
                string roomName = string.Format("({0}, {1}, {2})", position.x, position.y, position.z);
                foreach (GameObject gObj in roomList)
                {
                    if (gObj.name == roomName)
                    {
                        roomList.Remove(gObj);
                        roomPosList.Remove(gObj.transform.position);
                        Destroy(gObj);
                        break;
                    }
                }
                string gridName = string.Format("Tile ({0}, {1})", (int)position.z / 10, (int)position.x / 10);
                foreach (GameObject gObj in gridArray)
                {
                    if (gObj.name == gridName)
                    {
                        gObj.GetComponent<MeshRenderer>().enabled = true;
                        break;
                    }
                }
            }
        }
    }

    private void RemoveRoomSquare(int sizeX, int sizeZ)
    {
        if (sizeX % 2 != 0 && sizeZ % 2 != 0)
        {
            if (Input.GetMouseButton(1))
            {
                if (!Miscellaneous.IsPointerOverUIElement())
                {
                    GameObject tile = GetTileSingular();
                    if (tile != null)
                    {
                        int xLimit = Mathf.FloorToInt(sizeX / 2) * 10;
                        int zLimit = Mathf.FloorToInt(sizeZ / 2) * 10;
                        for (int x = -xLimit; x <= xLimit; x += 10)
                        {
                            for (int z = -zLimit; z <= zLimit; z += 10)
                            {
                                Vector3 position = GetTileInBoundaries(z, x, tile.transform);
                                RemoveRoom(position);
                            }
                        }
                    }
                }
            }
        }
    }

    private void RemoveRoomStar(int radius)
    {
        if (Input.GetMouseButton(1))
        {
            if (!Miscellaneous.IsPointerOverUIElement())
            {
                GameObject tile = GetTileSingular();
                if (tile != null)
                {
                    int xLimit = radius * 10;
                    int zLimit = 0;
                    for (int x = -xLimit; x <= xLimit + 20; x += 10)
                    {
                        for (int z = -zLimit; z <= zLimit; z += 10)
                        {
                            Vector3 position = GetTileInBoundaries(z, x, tile.transform);
                            RemoveRoom(position);
                        }
                        if (x >= 0)
                        {
                            zLimit -= 10;
                        }
                        else
                        {
                            zLimit += 10;
                        }
                    }
                }
            }
        }
    }

    private void RoomHologram(Vector3 position)
    {
        if (position.x != -1)
        {
            if (!hologramRoomPosList.Contains(position))
            {
                GameObject hologram = GameObject.Instantiate(roomPrefabsHologramArray[floorRoomIndex.Key][floorRoomIndex.Value], position, Quaternion.identity, roomParent.transform);
                hologram.name = string.Format("Hologram: ({0}, {1})", position.x, position.z);
                hologramRoomList.Add(hologram);
                hologramRoomPosList.Add(position);
            }
        }    
    }

    private void ResetHologram()
    {
        hologramRoomPosList.Clear();
        for(int i = 0; i < hologramRoomList.Count; i++)
        {
            Destroy(hologramRoomList[i]);
        }
        hologramRoomList.Clear();
    }

    private void RoomHologramSquare(int sizeX, int sizeZ)
    {
        if (sizeX % 2 != 0 && sizeZ % 2 != 0)
        {
            ResetHologram();
            if (!Miscellaneous.IsPointerOverUIElement())
            {
                GameObject tile = GetTileSingular();
                if (tile != null)
                {
                    int xLimit = Mathf.FloorToInt(sizeX / 2) * 10;
                    int zLimit = Mathf.FloorToInt(sizeZ / 2) * 10;
                    for (int x = -xLimit; x <= xLimit; x += 10)
                    {
                        for (int z = -zLimit; z <= zLimit; z += 10)
                        {
                            Vector3 position = GetTileInBoundaries(z, x, tile.transform);
                            RoomHologram(position);
                        }
                    }
                }
            }
        }
    }

    private void RoomHologramStar(int radius)
    {
        ResetHologram();
        if (!Miscellaneous.IsPointerOverUIElement())
        {
            GameObject tile = GetTileSingular();
            if (tile != null)
            {
                int xLimit = radius * 10;
                int zLimit = 0;
                for (int x = -xLimit; x <= xLimit + 20; x += 10)
                {
                    for (int z = -zLimit; z <= zLimit; z += 10)
                    {
                        Vector3 position = GetTileInBoundaries(z, x, tile.transform);
                        RoomHologram(position);
                    }
                    if (x >= 0)
                    {
                        zLimit -= 10;
                    }
                    else
                    {
                        zLimit += 10;
                    }
                }
            }
        }
    }

    private void InstantiateRoomParent()
    {
        roomParent = new GameObject("Rooms");
        roomParent.transform.position = new Vector3(0, 0, 0);
        roomParent.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    
    private void InstantiateGrid()
    {
        GameObject gridParent = new GameObject("Grid");
        gridArray = new GameObject[size * size];
        gridParent.transform.position = new Vector3(0, 0, 0);
        gridParent.transform.rotation = Quaternion.Euler(0, 0, 0);
        int i = 0;
        for(int x = -size * 10 / 2; x < size * 10 / 2; x += 10)
        {
            for(int z = -size * 10 / 2; z < size * 10 / 2; z += 10)
            {
                gridArray[i] = GameObject.Instantiate(basePrefab, new Vector3(x, 0, z), Quaternion.identity, gridParent.transform);
                gridArray[i].name = string.Format("Tile ({0}, {1})", z / 10, x / 10);
                i++;
            }
        }
        SetCameraLimits();
    }

    private void SetCameraLimits()
    {
        editorCamMovScript.SetLimits(size * 10 / 2, size * 10 / 2);
    }

    private void InitRoomPrefabsArray()
    {
        roomPrefabsArray = new Dictionary<int, GameObject>[1]
        {
            new Dictionary<int, GameObject>()
            {
                {-1,  f0R1n},
                {0,  f0R0},
                {1,  f0R1},
                {2,  f0R2},
            }
        };
    }

    private void InitRoomPrefabsHologramArray()
    {
        roomPrefabsHologramArray = new Dictionary<int, GameObject>[1]
        {
            new Dictionary<int, GameObject>()
            {
                {-1,  h_f0R1n},
                {0,  h_f0R0},
                {1,  h_f0R1},
                {2,  h_f0R2},
            }
        };
    }

    private void AdjustRoomPrefabsForEditor()
    {
        foreach(Dictionary<int, GameObject> floor in roomPrefabsArray)
        {
            foreach(KeyValuePair<int, GameObject> room in floor)
            {
                foreach(MeshCollider mC in room.Value.GetComponentsInChildren<MeshCollider>())
                {
                    mC.enabled = false;
                }
                foreach (BoxCollider bC in room.Value.GetComponentsInChildren<BoxCollider>())
                {
                    bC.enabled = false;
                }
            }
        }
    }

    private void Awake()
    {
        InstantiateRoomParent();
        InstantiateGrid();
        InitRoomPrefabsArray();
        InitRoomPrefabsHologramArray();
        AdjustRoomPrefabsForEditor();
        roomList = new List<GameObject>();
        roomPosList = new List<Vector3>();
        hologramRoomList = new List<GameObject>();
        hologramRoomPosList = new List<Vector3>();
        floorRoomIndex = new KeyValuePair<int, int>(0, 0);
    }


    private void Update()
    {
        if (brushIndex == 0)
        {
            int[] dimensions = editorMenusScript.GetSquareDimensions();
            RoomHologramSquare(dimensions[1], dimensions[0]);
            PlaceRoomSquare(dimensions[1], dimensions[0]);
            RemoveRoomSquare(dimensions[1], dimensions[0]);
        }
        else if(brushIndex == 1)
        {
            int radius = editorMenusScript.GetStarRadius();
            RoomHologramStar(radius);
            PlaceRoomStar(radius);
            RemoveRoomStar(radius);
        }
    }
}
