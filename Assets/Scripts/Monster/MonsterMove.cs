using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    public Vector3[] wayPoints;
    private Vector3 curPosition;

    public float Speed;
    public int Soul;

    private int wayPointIndex;

    public void Init(int speed, int soul)
    {
        Speed = speed;
        Soul = soul;

        transform.localScale = new Vector3(-2, 2, -1);

        wayPointIndex = 0;
        wayPoints = new Vector3[4];

        wayPoints.SetValue(new Vector3(0, -7.0f, transform.position.z), 0);
        wayPoints.SetValue(new Vector3(13f, -0.4f, transform.position.z), 1);
        wayPoints.SetValue(new Vector3(0, 6.0f, transform.position.z), 2);
        wayPoints.SetValue(new Vector3(-13f, -0.4f, transform.position.z), 3);
    }

    void Update()
    {
        if (wayPointIndex < wayPoints.Length)
        {
            float step = Speed * Time.deltaTime;

            curPosition = transform.position;
            transform.position = Vector3.MoveTowards(curPosition, wayPoints[wayPointIndex], step);

            if (Vector3.Distance(wayPoints[wayPointIndex], curPosition) == 0.0f)
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
