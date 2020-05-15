using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    public Vector3[] wayPoints;
    private Vector3 currPosition;

    private int wayPointIndex = 0;
    public float speed;

    void Start()
    {
        wayPoints = new Vector3[4];

        wayPoints.SetValue(new Vector3(0, -7.0f, transform.position.z), 0);
        wayPoints.SetValue(new Vector3(13f, -0.4f, transform.position.z), 1);
        wayPoints.SetValue(new Vector3(0, 6.0f, transform.position.z), 2);
        wayPoints.SetValue(new Vector3(-13f, -0.4f, transform.position.z), 3);
    }

    void Update()
    {
        currPosition = transform.position;

        if (wayPointIndex < wayPoints.Length)
        {
            float step = speed * Time.deltaTime;

            transform.position = Vector3.MoveTowards(currPosition, wayPoints[wayPointIndex], step);

            if (Vector3.Distance(wayPoints[wayPointIndex], currPosition) == 0.0f)
            {
                wayPointIndex++;

                if (wayPointIndex == 2) transform.localScale = new Vector3(2, 2, -1);
                if (wayPointIndex > 3)
                {
                    wayPointIndex = 0;
                    transform.localScale = new Vector3(-2, 2, -1);
                }
            }
        }

    }
}
