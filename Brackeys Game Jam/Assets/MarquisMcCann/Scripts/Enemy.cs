using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float walkSpeed;
    public float runSpeed;

    Rigidbody2D rb;

    [Header("wayPoints")]
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
        rb = GetComponent<Rigidbody2D>();

        nextWayPoint = 1;
       
        totalWayPoints = patrolPoints.Length;

        delayWayPoint = delayWayPointTime;

        target = patrolPoints[nextWayPoint];
    }

    // Update is called once per frame
    void Update()
    {
        target = patrolPoints[nextWayPoint];
        //movement();
        
    }

    private void FixedUpdate()
    {
        movement();
    }

    void movement()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
     
        if (Vector2.Distance(transform.position, patrolPoints[nextWayPoint].position) < distanceTo && nextWayPoint < totalWayPoints)
        {
            /*if (delayWayPoint <= 0)
            {
                
                delayWayPoint = delayWayPointTime;
            }
            else
            {
                delayWayPoint -= Time.deltaTime;
            }*/

            nextWayPoint++;
        }

        if (nextWayPoint == totalWayPoints)
        {
            nextWayPoint = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerStates>().SetState(PlayerStates.PlayerStateMachine.Dead);
            Destroy(gameObject);
        }
    }
}
