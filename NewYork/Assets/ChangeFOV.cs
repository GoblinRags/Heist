using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFOV : MonoBehaviour
{
    public float MinFov = 30f;

    public float MaxFov = 90f;

    public int Dir = 1;
    public float ChangeFov = .03f;
    public Camera Cam;
    // Start is called before the first frame update
    void Start()
    {
        Cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Cam.fieldOfView >= MaxFov && Dir == 1)
        {
            Dir = -1;
        }
        else
        {
            Cam.fieldOfView += Dir * ChangeFov;
        }
        
        if (Cam.fieldOfView <= MinFov && Dir == -1)
        {
            Dir = 1;
        }
        else
        {
            Cam.fieldOfView += Dir * ChangeFov;
        }
    }
}
