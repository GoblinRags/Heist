using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastGrounded : MonoBehaviour
{
    public float CastingDistance = 4f;
    // Update is called once per frame
    void Update()
    {
        //1 declare a ray. at the point of origin and point it Down
        
        Ray myRay = new Ray(transform.position, Vector3.down);
        
        //2 set the max distance
        
        //3 draw debug line that shows the ray
        Debug.DrawRay(myRay.origin, myRay.direction * CastingDistance, Color.magenta);
        if (Physics.Raycast(myRay, CastingDistance))
        {
            Debug.Log("HIT GROUND");
        }
        else
        {
            transform.Rotate(0,5f, 0f);
        }
    }
}
