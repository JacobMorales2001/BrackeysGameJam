using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour
{
    public Respawn respawn;
    public bool isDead;
    public bool canRespawn;

    [HideInInspector]
    public enum PlayerStateMachine
    {
        Alive,
        Dead,

        NUM_STATES
    }

    public PlayerStateMachine currState = PlayerStateMachine.Alive;

    Dictionary<PlayerStateMachine, Action> playerFsm = new Dictionary<PlayerStateMachine, Action>();

    // Start is called before the first frame update
    void Start()
    {
        playerFsm.Add(PlayerStateMachine.Alive, new Action(AliveState));
        playerFsm.Add(PlayerStateMachine.Dead, new Action(DeadState));

        SetState(PlayerStateMachine.Alive);

        respawn = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Respawn>();
    }

    public void SetState(PlayerStateMachine newState)
    {
        currState = newState;
        Debug.Log(currState);
    }

    void AliveState()
    {

    }

    void DeadState()
    {
        canRespawn = false;

        

        if (!isDead && !canRespawn)
        {
            GetComponent<PlayerGrab>().enabled = false;
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<CharacterController2D>().enabled = false;
            GetComponent<PlayerInput>().enabled = false;
            GetComponent<Rigidbody2D>().freezeRotation = false;
            GetComponent<RewindPlayer>().enabled = false;
            GetComponent<PlayerReverseGrapplingHook>().enabled = false;

            GetComponent<SpriteRenderer>().color = Color.gray;
            gameObject.AddComponent<Grabbable>();
            GetComponent<CircleCollider2D>().enabled = true;


            canRespawn = true;
            isDead = true;
            addPlayerToDeadList();
        }      
    }

    void addPlayerToDeadList()
    {
        respawn.DeadPlayers.Add(gameObject);  
    }

    // Update is called once per frame
    void Update()
    {
        playerFsm[currState].Invoke();
    }

    private void OnDestroy()
    {
        respawn.DeadPlayers.Remove(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Bullet")
        {
            SetState(PlayerStateMachine.Dead);
        }
    }
}
