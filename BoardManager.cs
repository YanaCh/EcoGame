using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine;
using System.Linq;
using UnityEngine.Diagnostics;

public class BoardManager : MonoBehaviour {

    // Using Serializable allows us to embed a class with sub properties in the inspector.
    [Serializable]
    public class Count
    {
        public int minimum;             //Minimum value for our Count class.
        public int maximum;             //Maximum value for our Count class.


        //Assignment constructor.
        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }
   
    public int columns = 6;                                         //Number of columns in our game board.
    public int rows = 4;                                            //Number of rows in our game board.
    public Count barrelCount = new Count(1, 5);                      //Lower and upper limit for our random number of walls per level.
    public Count bagsCount = new Count(1, 4);                      //Lower and upper limit for our random number of food items per level.
    public List<GameObject> barrels;                                 //Array of floor prefabs.
    public List<GameObject> bags;
    public GameObject floor;
   

    private List<Vector3> gridPositions = new List<Vector3>();   //A list of possible locations to place tiles.//Array of wall prefabs.
    private Transform boardHolder;
    
   
    void CalculateFloorEndPoints(out Vector3 floorLeftEndPoint, out Vector3 floorRightEndPoint)
    {
     
        float floorCenterY = floor.transform.position.y + floor.transform.localScale.y / 2;
        floorLeftEndPoint.x = floor.transform.position.x - floor.transform.localScale.x / 2;
        floorLeftEndPoint.z = floor.transform.position.z + floor.transform.localScale.z / 2;
        floorLeftEndPoint.y = floorCenterY;
        floorRightEndPoint.x = floor.transform.position.x + floor.transform.localScale.x / 2;
        floorRightEndPoint.z = floor.transform.position.z - floor.transform.localScale.z / 2;
        floorRightEndPoint.y = floorCenterY;

       
    }

    void InitialiseList()
    {
        Vector3 floorLeftEndPoint;
        Vector3 floorRightEndPoint;

        CalculateFloorEndPoints(out floorLeftEndPoint, out floorRightEndPoint);

        gridPositions.Clear();

        for (float i = floorLeftEndPoint.x; i < floorRightEndPoint.x; i += 0.01f)
        {
            for (float j = floorLeftEndPoint.z; j > floorRightEndPoint.z; j -= 0.01f)
            {
                gridPositions.Add(new Vector3(i, floorLeftEndPoint.y, j));
               
            }
        }
        

    }

       
    Vector3 RandomPosition(out int randomIndex)
    {
        randomIndex = Random.Range(0, gridPositions.Count);
        Vector3 randomPosition = gridPositions[randomIndex];
        return randomPosition;
    }

    
    void LayoutObjectAtRandom(List<GameObject> tileArray, int minimum, int maximum, int level)
    {
       // int objectsCount = Random.Range(minimum, maximum );
        int objectsCount = (int)Mathf.Log(level, 2f);
        Vector3 randomPosition;
        GameObject tileChoice;
        int randomIndex;

        for (int i = 0; i<objectsCount; i++)
        {           
            do
            {
                randomPosition = RandomPosition(out randomIndex);
                tileChoice = tileArray[Random.Range(0, tileArray.Count)];
            }
            while (!OnColision(randomPosition, tileChoice));

            gridPositions.RemoveAt(randomIndex);
            Instantiate(tileChoice, randomPosition, tileArray.First().transform.rotation);
        }

    }

    bool OnColision(Vector3 randomPosition, GameObject tileChoice)
    {
        //Debug.Log(tileChoice.GetComponent<MeshFilter>().sharedMesh.bounds.size);
        var raycastHits = Physics.OverlapBox(randomPosition, tileChoice.GetComponent<MeshFilter>().sharedMesh.bounds.size / 2, Quaternion.identity);

      //  Debug.Log(randomPosition);
       // Debug.Log(raycastHits.Length);

        foreach(var hit in raycastHits)
        {
           // Debug.Log(hit.transform.tag);
            if(hit.transform.tag != "Floor" && hit.transform.tag != "PositionsGrid")
            
                return false;   
        }

        return true;

    }

    public void SetupScene(int level)
    {
        InitialiseList();
        LayoutObjectAtRandom(barrels, barrelCount.minimum, barrelCount.maximum, level);
        LayoutObjectAtRandom(bags, bagsCount.minimum, bagsCount.maximum, level);
    }




}
