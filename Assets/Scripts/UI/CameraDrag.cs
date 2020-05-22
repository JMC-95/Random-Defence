using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    private Vector3 MouseStart;
    private Vector3 MouseMove;

    private float zoomSpeed = 10.0f;
    private float dist;

    void Start()
    {
        dist = transform.position.z;  // Distance camera is above map
    }

    void Update()
    {
        Move();
        Zoom();
    }

    void Move()
    {
        if (Camera.main.orthographicSize <= 5)
        {
            if (Input.GetMouseButtonDown(1))
            {
                MouseStart = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
                MouseStart = Camera.main.ScreenToWorldPoint(MouseStart);
                MouseStart.z = transform.position.z;

            }
            else if (Input.GetMouseButton(1))
            {
                MouseMove = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
                MouseMove = Camera.main.ScreenToWorldPoint(MouseMove);
                MouseMove.z = transform.position.z;

                transform.position = transform.position - (MouseMove - MouseStart);

                //Camera X축 이동 제한
                if (transform.position.x < -4.0f) transform.position = new Vector3(-4.0f, transform.position.y, dist);
                else if (transform.position.x > 4.0f) transform.position = new Vector3(4.0f, transform.position.y, dist);

                //Camera Y축 이동 제한
                if (transform.position.y < -2.5f) transform.position = new Vector3(transform.position.x, -2.5f, dist);
                else if (transform.position.y > 5.0f) transform.position = new Vector3(transform.position.x, 5.0f, dist);
            }
        }
        else
        {
            transform.position = new Vector3(0, -0.3f, dist);
        }
    }

    void Zoom()
    {
        float distance = Input.GetAxis("Mouse ScrollWheel") * -1 * zoomSpeed;

        if (distance != 0)
        {
            Camera.main.orthographicSize += distance;

            //Camera 확대 및 축소 제한
            if (Camera.main.orthographicSize < 3) Camera.main.orthographicSize = 3;
            else if (Camera.main.orthographicSize > 7) Camera.main.orthographicSize = 7;
        }
    }
}