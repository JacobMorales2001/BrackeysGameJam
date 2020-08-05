using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindEnemy : MonoBehaviour
{
    GameObject player;

    public float rewindDistance; // closest the player can get to the enemy before they get rewinded.

    RewindPlayer rewindPlayer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rewindPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<RewindPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        checkRewindDistance();
    }

    // when the player gets close to the enemy the player will rewind back a couple of seconds
    void checkRewindDistance()
    {
        
        if (Vector2.Distance(transform.position, player.transform.position) <= rewindDistance && !rewindPlayer.canRewind)
        {
            Debug.Log("rewind");
            rewindPlayer.canRewind = true;
            rewindPlayer.RewindBack();
        }
        else
        {
            rewindPlayer.canRewind = false;
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
