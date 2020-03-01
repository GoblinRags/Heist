using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RaycastGun : MonoBehaviour
{

    public float MaxRayDistance;
    
    public Camera Cam;
    public GameObject Canvas;
    

    public Vector3 RaycastedObj;
    // Start is called before the first frame update
    void Start()
    { 
        
        Cursor.visible = false;
        Cam = Camera.main;
        
    }

    // Update is called once per frame
    void Update()
    {
        //1 define the ray
        //screen point to ray
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        //2 max raycast distance
        
        
        //3 visualize the raycast
        Debug.DrawRay(camRay.origin, camRay.direction * MaxRayDistance, Color.green);
        //4a definite object detction
        RaycastHit hitObj;

        //4 detect object with the ray
        if (Physics.Raycast(camRay, out hitObj, MaxRayDistance))
        {
            //5 when hit, do something useful (useful)
            RaycastedObj = hitObj.point;
        }
        else
        {
            RaycastedObj = Vector3.zero;
        }
    }
}

