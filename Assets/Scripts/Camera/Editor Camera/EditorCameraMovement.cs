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

    [Header("Screen Dimensions")]
    public int screenX = 1920;
    public int screenY = 1080;

    private float horizontal;
    private float vertical;
    private float zoom;
    private float boost;
    private Vector3 mousePos;

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
        if (!Miscellaneous.IsPointerOverUIElement())
        {
            zoom = Input.GetAxis("Mouse ScrollWheel");
            if (zoom != 0)
            {
                Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit))
                {
                    if(zoom > 0)
                    {
                        if (hit.distance > 20)
                        {
                            transform.position = Vector3.MoveTowards(transform.position, hit.point, zoomSpeed);
                        }
                    }
                    else
                    {
                        if (hit.distance < 100)
                        {
                            transform.position = Vector3.MoveTowards(transform.position, hit.point, -zoomSpeed);
                        }
                    }
                }
            }
        }
    }

    private void MouseMovement()
    {
        mousePos = Input.mousePosition;
        Debug.Log("NEW LINE");
        if(mousePos.x >= Screen.width - Screen.width / 20)
        {
            Debug.Log("MOVE RIGHT");
            if (mousePos.y >= Screen.height / 2)
            {
                Debug.Log("MOVE UP");
                transform.Translate(new Vector3(-Time.deltaTime * cameraSpeed * (mousePos.y * 0.4f / Screen.height) / 3, 0, Time.deltaTime * cameraSpeed * (mousePos.x * 0.4f / Screen.width) / 5), Space.World);
            }
            else if (mousePos.y < Screen.height / 2)
            {
                Debug.Log("MOVE DOWN");
                transform.Translate(new Vector3(Time.deltaTime * cameraSpeed * ((Screen.height - mousePos.y * 2.5f) / Screen.height) / 3, 0, Time.deltaTime * cameraSpeed * (mousePos.x * 0.4f / Screen.width) / 5), Space.World);
            }
            else
            {
                transform.Translate(new Vector3(0, 0, Time.deltaTime * cameraSpeed * (mousePos.x * 0.4f / Screen.width) / 5), Space.World);
            }

        }
        else if (mousePos.x <= Screen.width / 20)
        {
            Debug.Log("MOVE LEFT");
            if (mousePos.y >= Screen.height / 2)
            {
                Debug.Log("MOVE UP");
                transform.Translate(new Vector3(-Time.deltaTime * cameraSpeed * (mousePos.y * 0.4f / Screen.height) / 3, 0, -Time.deltaTime * cameraSpeed * ((Screen.width - mousePos.x * 2.5f) / Screen.width) / 5), Space.World);
            }
            else if (mousePos.y < Screen.height / 2)
            {
                Debug.Log("MOVE DOWN");
                transform.Translate(new Vector3(Time.deltaTime * cameraSpeed * ((Screen.height - mousePos.y * 2.5f) / Screen.height) / 3, 0, -Time.deltaTime * cameraSpeed * ((Screen.width - mousePos.x * 2.5f) / Screen.width) / 5), Space.World);
            }
            else
            {
                transform.Translate(new Vector3(0, 0, -Time.deltaTime * cameraSpeed * ((Screen.width - mousePos.x * 2.5f) / Screen.width) / 5), Space.World);
            }

        }
        else if (mousePos.y >= Screen.height - Screen.height / 20)
        {
            Debug.Log("MOVE UP");
            if (mousePos.x >= Screen.width / 2)
            {
                Debug.Log("MOVE RIGHT");
                transform.Translate(new Vector3(-Time.deltaTime * cameraSpeed * (mousePos.y * 0.4f / Screen.height) / 3, 0, Time.deltaTime * cameraSpeed * (mousePos.x * 0.4f / Screen.width) / 5), Space.World);
            }
            else if (mousePos.x < Screen.width/ 2)
            {
                Debug.Log("MOVE LEFT");
                transform.Translate(new Vector3(-Time.deltaTime * cameraSpeed * (mousePos.y * 0.4f / Screen.height) / 3, 0, -Time.deltaTime * cameraSpeed * ((Screen.width - mousePos.x * 2.5f) / Screen.width) / 5), Space.World);
            }
            else
            {
                transform.Translate(new Vector3(-Time.deltaTime * cameraSpeed * (mousePos.y * 0.4f / Screen.height) / 3, 0, 0), Space.World);
            }
        }
        else if (mousePos.y < Screen.height / 20)
        {
            Debug.Log("MOVE DOWN");
            if (mousePos.x >= Screen.width / 2)
            {
                Debug.Log("MOVE RIGHT");
                transform.Translate(new Vector3(Time.deltaTime * cameraSpeed * ((Screen.height - mousePos.y * 100f) / Screen.height) / 3, 0, Time.deltaTime * cameraSpeed * (mousePos.x * 0.4f / Screen.width) / 5), Space.World);
            }
            else if (mousePos.x < Screen.width / 2)
            {
                Debug.Log("MOVE LEFT");
                transform.Translate(new Vector3(Time.deltaTime * cameraSpeed * ((Screen.height - mousePos.y * 100f) / Screen.height) / 3, 0, -Time.deltaTime * cameraSpeed * ((Screen.width - mousePos.x * 2.5f) / Screen.width) / 5), Space.World);
            }
            else
            {
                transform.Translate(new Vector3(Time.deltaTime * cameraSpeed * ((Screen.height - mousePos.y * 100f) / Screen.height) / 3, 0, 0), Space.World);
            }
        }
    }

    public void SetLimits(int x, int z)
    {
        xLimit = x;
        zLimit = z;
    }

    private void Update()
    {
        MouseMovement();
        Move();
        Zoom();
    }
}
