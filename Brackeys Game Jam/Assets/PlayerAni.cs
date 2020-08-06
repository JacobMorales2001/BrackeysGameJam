using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAni : MonoBehaviour
{
    PlayerMovement play;
    Animator anime;
    CharacterController2D cc2;
    int aniControl = 0;
    //AniControl|| 0 = Idle | 1 = Walk | 2 = Jump | 3 = Fall | 4 = Land | 5 = Grab | 6 = Throw | //
    // Start is called before the first frame update
    void Start()
    {
        play = GetComponent<PlayerMovement>();
        anime = GetComponent<Animator>();
        cc2 = GetComponent<CharacterController2D>();
    }

    // Update is called once per frame
    void Update()
    {
        anime.SetInteger("Ani", aniControl);
        if (play.PlayerCanMove)
        {
            if(cc2.m_Rigidbody2D.velocity.y <= 0.1 || cc2.m_Rigidbody2D.velocity.y >= -0.1)
            {
                if (Input.GetAxisRaw("Horizontal") > 0.2 || Input.GetAxisRaw("Horizontal") < -0.2)
                {
                    aniControl = 1;
                    //anime.SetInteger("Ani", 1);
                    //anime.Play("Walk");
                }
                else if (Input.GetAxisRaw("Horizontal") <= 0.2 && Input.GetAxisRaw("Horizontal") >= -0.2)
                {
                    aniControl = 0;
                    //anime.SetInteger("Ani", 0);
                    //anime.Play("Idle");
                }
            }
            if (cc2.m_Rigidbody2D.velocity.y > 0)
            {
                aniControl = 2;
                //anime.SetInteger("Ani", 2);
                //anime.Play("Jump");

            }
            else if (cc2.m_Rigidbody2D.velocity.y < 0)
            {
                if (cc2.m_Grounded)
                {
                    aniControl = 0;
                    //anime.SetInteger("Ani", 4);
                    //anime.Play("Land");
                }
                else
                {
                    aniControl = 3;
                    //anime.SetInteger("Ani", 3);
                    //anime.Play("Fall");
                }
                
            }

        }
    }
}
