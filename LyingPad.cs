using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LyingPad : MonoBehaviour {
    public bool stay = true;
    private float stayCount = 0.0f;

    private void OnTriggerEnter(Collider other)
    {
        var garbage = other.GetComponent<GarbageThrow>();

        if (garbage != null)
        {
            garbage.OnSuccesesfullHit?.Invoke();
        }
        //if (stay)
        //{
        //    if (stayCount > 5f)
        //    {
        //        Debug.Log("staying");
        //        stayCount = stayCount - 5f;
        //    }
        //    else
        //    {
        //        stayCount = stayCount + Time.deltaTime;
        //    }
        //}

    }
}
