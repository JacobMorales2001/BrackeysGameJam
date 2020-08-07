using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ibButtonScript : MonoBehaviour
{
    public GameObject endDoor;

    private Animator buttonAnim, doorAnim;


    void Start()
    {
        buttonAnim = GetComponent<Animator>();
        doorAnim = endDoor.GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        buttonAnim.SetBool("ButtonHit", true);
    }

    public void ButtonPressed()
    {
        if (doorAnim.GetBool("ButtonPressed") == false)
        {
            doorAnim.SetBool("ButtonPressed", true);
        }
        else return;
    }
}
