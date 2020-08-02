using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public List<GameObject> DeadPlayers = new List<GameObject>();

    public GameObject playerPrefab;
    public Transform startPosition;

    // Start is called before the first frame update
    void Start()
    {
        
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
            if (DeadPlayers[i].GetComponent<PlayerStates>().canRespawn)
            {
                Instantiate(playerPrefab, startPosition);
            }
        }
    }
}
