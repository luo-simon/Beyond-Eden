using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Z_Update : MonoBehaviour
{
    private Transform pos;
    
    void Start()
    {
        pos = GetComponent<Transform>();
    }

    void Update()
    {
        pos.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
    }
}
