using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//only for mouse and keyboard for now
public class PlayerMovement : MonoBehaviour
{
    [Header("Misc Settings")]
    public Main mainScript;
    public Camera camera;
    public GameObject playerPrefab;

    private RoomsList roomList;

    private void GetRoomList()
    {
        roomList = mainScript.GetRoomsList();
    }

    private Room SelectRoom()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            foreach(Transform gObj in hit.transform.GetComponentsInParent<Transform>())
            {
                if(gObj.tag == "Room")
                {
                    return roomList.GetRoomByPosition(gObj.position);
                }
            }
        }
        return null;
    }

    private void Move()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Room room = SelectRoom();
            if(room != null)
            {
                
            }
        }
    }

    private void Start()
    {
        GetRoomList();
    }

    private void Update()
    {
        Move();
    }
}
