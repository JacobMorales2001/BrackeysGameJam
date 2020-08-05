using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindPlayer : MonoBehaviour
{
    public Transform rewindPoint;

    public GameObject[] RewindEnemies; // rewind enemies in the scene 

    public bool canRewind;

    public float distToDropRP;
    public bool canDropRP;

    // Start is called before the first frame update
    void Start()
    {
        RewindEnemies = GameObject.FindGameObjectsWithTag("RewindEnemy");
    }

    // Update is called once per frame
    void Update()
    {
        DroppingRewindPoint();
    }

    void DroppingRewindPoint()
    {

        for (int i = 0; i < RewindEnemies.Length; i++)
        {
            if (RewindEnemies[i] != null)
            {
                if (Vector2.Distance(transform.position, RewindEnemies[i].transform.position) >= distToDropRP && !canDropRP)
                {
                    rewindPoint.position = gameObject.transform.position;
                    canDropRP = true;
                }
                else
                {
                    canDropRP = false;
                }
            }
        }
        
    }

    public void RewindBack()
    {
        gameObject.transform.position = rewindPoint.position;
    }
}
