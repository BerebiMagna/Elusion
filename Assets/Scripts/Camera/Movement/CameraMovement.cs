using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float minZoom = 10;
    public float maxZoom = 100;
    private Transform player;
    float scrollAxis;


    public void SetPlayer(Transform player)
    {
        this.player = player;
    }

    private void SetStartPos()
    {
        transform.position = Map.GetSpawnPoint();
        transform.Translate(new Vector3(0, 30, 0), Space.World);
    }

    private void Scroll()
    {
        scrollAxis = Input.GetAxis("Mouse ScrollWheel") / 2;
        if(scrollAxis != 0)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(scrollAxis * transform.position.y / maxZoom + transform.position.y, minZoom, maxZoom), transform.position.z);
        }
    }

    private void TrackPlayer()
    {
        transform.position = new Vector3(player.position.x, transform.position.y, player.position.z);
    }

    private void Start()
    {
        //SetStartPos();
    }

    private void Update()
    {
        //TrackPlayer();
        Scroll();
    }
}
