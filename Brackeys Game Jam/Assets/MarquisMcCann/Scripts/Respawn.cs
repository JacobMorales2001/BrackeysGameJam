using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public List<GameObject> DeadPlayers = new List<GameObject>();
    public GameObject[] RespawnPlayers; 

    public GameObject playerPrefab; // player prefab
    public GameObject playerInGame; // player in the scene
    public Transform startPosition; // emepty game object start position for respawning player

    public int RespawnNum;

    // Start is called before the first frame update
    void Start()
    {
       //RespawnNum = RespawnPlayers.Length;
    }

    // Update is called once per frame
    void Update()
    {
        respawnPlayer();
    }

    void respawnPlayer()
    {
        for (int i = 0; i < DeadPlayers.Count; i++)
        {
            if (DeadPlayers[i].GetComponent<PlayerStates>().canRespawn == true)
            {
                RespawnNum++;

                // if the player die their will be another player to spawn and the old body will be in the same place.
                if (RespawnNum <= 4)
                {
                    Instantiate(playerPrefab, startPosition.position, Quaternion.identity);
                }

                // if the player dies 4 times then they will teleport back to the beginning when they die.
                if (RespawnNum >= 5)
                {
                    playerInGame.transform.position = startPosition.position;
                }
     
            }
        }
    }

    void GetPlayerInScene()
    {
        //playerInGame = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStates>().currState == PlayerStates.PlayerStateMachine.Alive;
    }

}
