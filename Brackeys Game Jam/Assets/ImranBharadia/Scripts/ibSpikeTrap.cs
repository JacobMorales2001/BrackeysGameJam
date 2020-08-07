using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ibSpikeTrap : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Vector3 zAxis;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        zAxis.z = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

}
