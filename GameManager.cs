using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private BoardManager boardScript;

	// Use this for initialization
	void Awake () {

	}

    private void Start()
    {
        boardScript = GetComponent<BoardManager>();
        boardScript.SetupScene(4);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
