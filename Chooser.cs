using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Chooser : MonoBehaviour {

    public Button btn;

    Color [] colorsMas = { Color.green, Color.blue, Color.red };
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void HideButton()
    {
        ColorBlock tmp = btn.colors;
        tmp.normalColor = colorsMas[Random.Range(0, colorsMas.Length)];
        btn.colors = tmp;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }


}
