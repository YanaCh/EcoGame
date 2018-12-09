using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamaController : MonoBehaviour {

    public static bool gameStarted = false;

    [Header("Controllers")]
    public Network network;
    public UIController uIController;

    [Space(10)]
    [Header("Game objects")]
    public GameObject ballSpawnPoint;

    public GarbageThrow ball;
    public LentMove lentMove;

    public Action OnGarbageCreated;
    public int score = 0;

    protected GarbageThrow currentGarb;
    protected GameObject[] garbages;
    protected List<GameObject> garbagesList;


   // protected List<GarbageThrow> garbeges;

    // Use this for initialization
    void Start () {
        lentMove.OnReachPoint += UnParent;
	    lentMove.OnThrow += ThrowAble;
        OnGarbageCreated += lentMove.StartMove;

        CreateBall();
	}

    // Update is called once per frame
    void Update()
    {
         garbages = GameObject.FindGameObjectsWithTag("Garbage");
         garbagesList = new List<GameObject>(garbages);

        if(garbagesList.Count > 8)
        {
            foreach(GameObject garb in garbagesList)
            {
               Destroy(garb);
                break;
            }
        }


    }

    void CreateBall()
    {
        if (currentGarb!= null)
        {
            currentGarb.enable = false;
        }

        currentGarb = Instantiate(ball, ballSpawnPoint.transform.position, ballSpawnPoint.transform.rotation);
        currentGarb.transform.SetParent(ballSpawnPoint.transform);
        currentGarb.GetComponent<Rigidbody>().isKinematic = true;
        currentGarb.OnCollision += CreateBall;
        currentGarb.OnSuccesesfullHit += AddScore;

        OnGarbageCreated();

        
    }

    public void AddScore()
    {
        score++;
        uIController.scoreText.text = score.ToString();
    }

    public void UnParent()
    {
        currentGarb.transform.SetParent(null);
        currentGarb.GetComponent<Rigidbody>().isKinematic = false;
    }

    void ThrowAble(bool reachedPoint)
    {
        if(reachedPoint)
        currentGarb.enableThrow = true;
        currentGarb.enable = true;
        //else
        //{
        //    currentGarb.enableThrow = false;
        //}
    }

    public void StartGame()
    {
        StartCoroutine(StartGameCoroutine());
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator StartGameCoroutine()
    {
        Debug.Log("Started counting");
        gameStarted = true;
        yield return new WaitForSeconds(35);
        network.End(score);
    }
}
