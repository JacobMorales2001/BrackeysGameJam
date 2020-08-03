using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    float delayDoor;
    public float delayDoorTime;

    public string moveDirection; /// <summary>
    /// Up, moves the door up
    /// Down, moves the door down
    /// Left. moves the door left
    /// Right, moves the door right
    /// </summary>
    

    // Start is called before the first frame update
    void Start()
    {
        delayDoor = delayDoorTime;
    }

    // Update is called once per frame
    void Update()
    {
        moveDoor();
    }

    void moveDoor()
    {
        if (moveDirection == "Up" && delayDoor >= 0)
        {
            delayDoor -= Time.deltaTime;
            transform.Translate(transform.up * Time.deltaTime);

            if (delayDoor <= 0)
            {
                delayDoor = delayDoorTime;
                transform.Translate(0, 0, 0);
            }
        }
    }
}
