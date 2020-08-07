using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ibButtonScript : MonoBehaviour
{
    public GameObject finishRoom;

    private Animator roomAnim;


    void Start()
    {
        roomAnim = finishRoom.GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (roomAnim.GetBool("ButtonHit") == false)
        {
            roomAnim.SetBool("ButtonHit", true);
        }
        else return;
    }
}
