using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LentMove : MonoBehaviour {

    public GameObject ballSpawnPoint;
    public GameObject ballMiddlePoint;

    public Action OnReachPoint;
    public Action<bool> OnThrow;
    // Use this for initialization
    protected Vector3 startingPoint;

    protected float speed = 1f;
    protected bool reachedPoint = true;

	void Start () {
        startingPoint = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

	    if (reachedPoint)
	    {
	        OnThrow(reachedPoint);
	        return;
        }

         
	    OnThrow(reachedPoint);
        transform.position += Vector3.right * speed * Time.deltaTime;
        if (Vector3.Distance(ballSpawnPoint.transform.position, ballMiddlePoint.transform.position) < 0.05f)
        {
         
            OnReachPoint();
            reachedPoint = true;
            transform.position = startingPoint;
        }
	}

    public void StartMove()
    {
        reachedPoint = false;
    }
}
