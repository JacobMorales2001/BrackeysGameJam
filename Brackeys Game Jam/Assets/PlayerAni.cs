using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAni : MonoBehaviour
{
    PlayerMovement play;
    Animator anime;
    // Start is called before the first frame update
    void Start()
    {
        play = GetComponent<PlayerMovement>();
        anime = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (play.PlayerCanMove)
        {
            if(Input.GetAxisRaw("Horizontal") > 0.2 || Input.GetAxisRaw("Horizontal") < -0.2)
            {
                anime.Play("Walk");
            }
            else if(Input.GetAxisRaw("Horizontal") <= 0.2 && Input.GetAxisRaw("Horizontal") >= -0.2)
            {
                anime.Play("Idle");
            }
        }
    }
}
