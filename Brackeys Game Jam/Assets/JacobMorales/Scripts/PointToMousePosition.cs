using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointToMousePosition : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.up);
    }
}
