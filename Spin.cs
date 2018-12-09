using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Spin : MonoBehaviour {

    public float speed = 30f;
    

    void Update()
    {
        //  transform.RotateAround(transform.position, transform.up, Time.deltaTime * 90f);
        transform.Rotate(0, 0, speed * Time.deltaTime);

    }
}
