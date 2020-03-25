using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorCameraMovement : MonoBehaviour
{
    [Header("Editor Camera Properties")]
    public float cameraSpeed = 10;
    public float zoomSpeed = 10;
    public float xLimit = 500;
    public float zLimit = 500;
    public float zoomMax = 100;
    public float zoomMin = 10;

    [Header("Controls")]
    public KeyCode boostKey = KeyCode.LeftShift;

    private float horizontal;
    private float vertical;
    private float zoom;
    private float boost;

    private void Move()
    {
        horizontal = Input.GetAxis("Horizontal") * Time.deltaTime * cameraSpeed;
        vertical = Input.GetAxis("Vertical") * Time.deltaTime * cameraSpeed;
        if (Input.GetKey(boostKey))
        {
            boost = 2;
        }
        else
        {
            boost = 1;
        }
        transform.position = new Vector3(Mathf.Clamp(transform.position.x + vertical * boost, -xLimit, xLimit), transform.position.y, Mathf.Clamp(transform.position.z + horizontal * boost, -zLimit, zLimit));
    }

    private void Zoom()
    {
        zoom = Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * zoomSpeed;
        if(zoom != 0)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y + zoom, zoomMin, zoomMax), transform.position.z);
        }
    }

    public void SetLimits(int x, int z)
    {
        xLimit = x;
        zLimit = z;
    }

    private void Update()
    {
        Move();
        Zoom();
    }
}
