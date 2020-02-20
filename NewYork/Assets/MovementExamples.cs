using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementExamples : MonoBehaviour
{
    
    //Third Person
    private Rigidbody RB;


    public KeyCode[] ForwardC;
    public KeyCode[] BackC;

    public KeyCode[] LeftC;

    public KeyCode[] RightC;

    public float ForwardBackward;

    public float RightLeft;
    public float Jump;
    public float Speed = 1f;

    public float RotationSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ForwardBackward = Input.GetAxis("Vertical");
        RightLeft = Input.GetAxis("Horizontal");
        Jump = Input.GetAxis("Jump");
    }
    
    

    private void FixedUpdate()
    {
        Vector3 force = ForwardBackward * Speed * transform.forward.normalized;
        
        RB.AddForce(force, ForceMode.Impulse);
        RB.AddForce(Vector3.up * Jump, ForceMode.Impulse);
        RB.AddTorque(new Vector3(0f, RightLeft * RotationSpeed, 0f));
    }

    bool CheckKeys(KeyCode[] keys)
    {
        foreach (KeyCode key in keys)
        {
            if (Input.GetKeyDown(key))
            {
                return true;
            }
        }
        return false;
    }
}
