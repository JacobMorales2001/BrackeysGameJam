using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory2 : MonoBehaviour
{
     public Vector2 Direction;
     private Vector2 startPos;
    public Vector2 endPos;
    public Vector2 initPos;
    private Rigidbody2D rigidbody;
    private Vector2 forceAtPlayer;
    public float forceFactor; 

    public GameObject trajectoryDot;
    private GameObject[] trajectoryDots;
    public int number;
    


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        trajectoryDots = new GameObject[number];

        // for(int i=0; i< number; i++)
        // {
        //     trajectoryDots[i] = Instantiate(trajectoryDot,transform.position, Quaternion.identity);
        // }


    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) { //click
            //startPos = gameObject.transform.position;
            for (int i = 0; i < number; i++)
            {
                trajectoryDots[i] = Instantiate(trajectoryDot, gameObject.transform);
            }
            
        }
        if(Input.GetMouseButton(0)) { //drag
            endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
           //gameObject.transform.position = endPos;
            forceAtPlayer = endPos - startPos;
            for (int i = 0; i < number; i++)
            {
                trajectoryDots[i].transform.position = calculatePosition(i * 0.03f);
            }
        }
        if(Input.GetMouseButtonUp(0)) { //leave
            // rigidbody.gravityScale = 1;
            // rigidbody.velocity = new Vector2(-forceAtPlayer.x * forceFactor, -forceAtPlayer.y * forceFactor);
            for (int i = 0; i < number; i++)
            {
                Destroy(trajectoryDots[i]);
            }
        }
        // if(Input.GetKey(KeyCode.Space))
        // {
        //     rigidbody.gravityScale = 0;
        //     rigidbody.velocity = Vector2.zero;
        //     gameObject.transform.position = initPos;
            
        // }
    }

    private Vector2 calculatePosition(float elapsedTime) 
    {
        return new Vector2(endPos.x, endPos.y) + //X0
                new Vector2(-forceAtPlayer.x * forceFactor, -forceAtPlayer.y * forceFactor) * elapsedTime + //ut
                0.5f * Physics2D.gravity * elapsedTime * elapsedTime ;
    }

    // Vector2 DotsPosition(float t)
    // {
    //     Vector2 currDotPosition = (Vector2)transform.position + (Direction.normalized * forceFactor * t) * 0.5f* Physics2D.gravity * (t*t);

    //     return currDotPosition;
    // }
   
}
