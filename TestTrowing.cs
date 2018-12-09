using UnityEngine;
using System.Collections;

public class TestTrowing : MonoBehaviour
{
    public Camera camera;
    public Rigidbody bullet;
    public LayerMask layer;
    public Transform shootPoint;
    
      
        private void Update()
        {
        LaunchProjectile();
        }

    void LaunchProjectile()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 targetPos = hit.point;

            Vector3 Vo = CalculateVelocity(targetPos, shootPoint.position, 1f);

            transform.rotation = Quaternion.LookRotation(Vo);

            if (Input.GetMouseButtonDown(0))
            {
                Rigidbody rb = Instantiate(bullet, shootPoint.position, Quaternion.identity);
                rb.velocity = Vo;
            }

            //Vector3 newPos = new Vector3(targetPos.x, Projectile.transform.position.y, targetPos.z);
            //  Projectile.transform.SetPositionAndRotation(newPos, Projectile.transform.rotation);

        }
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