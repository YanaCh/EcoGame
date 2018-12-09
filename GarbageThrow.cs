using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GarbageThrow : MonoBehaviour {
    
    protected Vector2 mouseStartingPoint;
    protected Vector3 startingPosition;

    public bool enable = false;//cant play witout another player
    public bool enableThrow = true; 
    public float windForce;
    public GameObject windIndecator;
   
    public event Action OnCollision;
    public Action OnSuccesesfullHit;

    private Grenade grenadeScript;
    private bool wasClicked = false;
    private bool wasMouseUp = false;
    private Rigidbody rb;

    void Start () {

        rb = GetComponent<Rigidbody>();
        startingPosition = transform.position;
        grenadeScript = GetComponent<Grenade>();
        windForce = 0;
      

    }

    private void FixedUpdate()
    {
      //  rb.position += Vector3.right * windForce;
    }


    void Update () {

        if (!enable || !GamaController.gameStarted)
            return;
        PositionCheck();

        if (wasClicked)
        {
            LaunchProjectile();

        }

    }



    void PositionCheck()
    {
        if (transform.position.y <= 0)
        {
           
            OnCollision();//чтобы когда бросил, сразу появился новый шарик
            enable = false;
           
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Lent" || !enable)
            return;
      
        if (collision.gameObject.tag == "Barrel")
        {
            GameObject barrel = collision.gameObject;
            grenadeScript.performExplosion();
            Destroy(barrel);
        }
         
        OnCollision();
        enable = false;
    }

    private void OnMouseDown()
    {
        if(!enableThrow || !enable)
            return;

        //wasClicked = true;
    }

    private void OnMouseUp()
    {
        if (!enableThrow || !enable)
            return;

        wasClicked = true;

        // Vector2 mouseVector = (mouseCurrentPoint - mouseStartingPoint).normalized;
        //Vector3 throwVector = new Vector3(mouseVector.x, mouseVector.y, 1);
        //  Vector3 throwVector = new Vector3(mouseVector.x, 1, mouseVector.y);
        // rigidbody.AddForce(throwVector * Vector2.Distance(mouseStartingPoint, mouseCurrentPoint));


        // enableThrow = false;
    }

    void LaunchProjectile()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 targetPos = hit.point;

           // targetPos += Vector3.right;

            Vector3 Vo = CalculateVelocity(targetPos, transform.position, 1f);

            // transform.rotation = Quaternion.LookRotation(Vo);
          

            if (Input.GetMouseButtonUp(0))
            {

                 rb.velocity = Vo;
                //rb.AddForce(Vo, ForceMode.VelocityChange);
                wasMouseUp = true;
               

            }
           // if(wasMouseUp)
           // transform.position += Vo * Time.deltaTime;
            // StartCoroutine(addWindForce());

            rb.position += Vector3.right * windForce;
           
            
                                            
        }
    }


    public void addWind()
    {
        windForce = 0.025f;
    }

    IEnumerator addWindForce(Vector3 Vo)
    {
        //if (transform.position.z >= windIndecator.transform.position.z)
           yield return transform.position += Vo * Time.deltaTime; ; 
        //yield return new WaitForSeconds(0.5f);
       // addWind();
        
    }

    Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float time)
    {
        Vector3 distance = target - origin;
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0f;

        float Sy = distance.y;
        float Sxz = distanceXZ.magnitude;

        float Vxz = Sxz / time;
        float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        Vector3 resultV = distanceXZ.normalized;
        resultV *= Vxz;
        resultV.y = Vy;


        return resultV;
    }

}
