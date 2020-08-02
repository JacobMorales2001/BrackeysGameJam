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
        isDead = true;
        canRespawn = true;
        if (isDead && canRespawn)
        {
            canRespawn = false;
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
}
