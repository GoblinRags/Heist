using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RaycastMouse : MonoBehaviour
{

    public float MaxRayDistance;
    public GameObject PaintPrefab;
    public GameObject PaintBrush;
    public Camera Cam;
    public GameObject Canvas;

    public float MinFov = 20f;

    public float MaxFov = 65f;

    public float ChangeFov = .3f;
    // Start is called before the first frame update
    void Start()
    { 
        
        Cursor.visible = false;
        Cam = GetComponent<Camera>();
        
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
            PaintBrush.transform.position = hitObj.point;
            if (Input.GetMouseButton(0))
            {
                GameObject paint = Instantiate(PaintPrefab, hitObj.point, Quaternion.identity);
                Color randC = Random.ColorHSV();
                paint.GetComponent<SpriteRenderer>().color = randC;
                paint.transform.SetParent(hitObj.transform);
            }
            
            //while hovering, spin the canvas
            hitObj.transform.Rotate(new Vector3(0f,0f, 45 * Time.deltaTime));
        }

        if (Cam.fieldOfView > MaxFov)
        {
            Cam.fieldOfView -= ChangeFov;
        }
        else if (Cam.fieldOfView < MinFov)
        {
            Cam.fieldOfView += ChangeFov;
        }
    }
}
