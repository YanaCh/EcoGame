using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverPad : MonoBehaviour {

    private GarbageThrow currentGarb;

    void Update()
    {

        

    }

    private void OnTriggerEnter(Collider other)
    {

            if (other.CompareTag("Garbage"))
        {
            currentGarb = other.GetComponent<GarbageThrow>();
            if(currentGarb != null)
            currentGarb.addWind();
            Debug.Log("Enerted WindZone");
        }
    }
}
