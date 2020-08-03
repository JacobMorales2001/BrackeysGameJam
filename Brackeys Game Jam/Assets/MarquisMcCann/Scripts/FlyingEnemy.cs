using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public float speed;

    [Header("WayPoints")]
    public Transform[] patrolPoints;
    public int totalWayPoints;
    public float distanceTo;
    int nextWayPoint;
    Transform target;
    float delayWayPoint;
    public float delayWayPointTime;

    // Start is called before the first frame update
    void Start()
    {
        nextWayPoint = 0;
        totalWayPoints = patrolPoints.Length;
        delayWayPoint = delayWayPointTime;
        target = patrolPoints[nextWayPoint];
    }

    // Update is called once per frame
    void Update()
    {
        target = patrolPoints[nextWayPoint];
        
    }

    private void FixedUpdate()
    {
        FlyMovement();
    }

    void FlyMovement()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, patrolPoints[nextWayPoint].position) < distanceTo && nextWayPoint < totalWayPoints)
        {
            nextWayPoint++;
        }

        if (nextWayPoint == totalWayPoints)
        {
            nextWayPoint = 0;
        }
    }
}
