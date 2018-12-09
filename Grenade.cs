using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour {

    public GameObject explosionEffect;
    public float radius = 0.5f;
    public float explosionForce = 20f;

    bool hasExploded = false;
    GameObject explosionEffectInstantiated;

    private void Awake()
    {
        explosionEffectInstantiated = Instantiate(explosionEffect, Vector3.one*100, Quaternion.identity);
    }


    public void performExplosion()
    {
        if ( !hasExploded)
        {
            Explode();
            hasExploded = true;
           
        }
    }

    void Explode()
    {
        explosionEffectInstantiated = Instantiate(explosionEffect, transform.position, transform.rotation);
        Debug.Log("BOOOM!");
        Collider[] colliders =  Physics.OverlapSphere(transform.position, radius);

        foreach(Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position,radius);
            }

            
        }

        Destroy(gameObject);
       
    }


}
