using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weighted : MonoBehaviour
{
    public float CurrentWeight;
    public float WeightSupported;
    public float WeightTilt;
    public Rigidbody RB;

    
    // Start is called before the first frame update
    void Start()
    {
        
        RB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision other)
    {
        bool checkWeight;
        if (other.gameObject.CompareTag("Cash"))
        {
            checkWeight = true;
            CurrentWeight += other.gameObject.GetComponent<Rigidbody>().mass;
            if (!RB.useGravity && CurrentWeight >= WeightTilt)
            {
                RB.constraints = RigidbodyConstraints.FreezePosition;
                if (CurrentWeight >= WeightSupported)
                {
                    RB.useGravity = true;
                    RB.constraints = RigidbodyConstraints.None;
                }
            }
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            CurrentWeight += other.gameObject.GetComponent<Rigidbody>().mass;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Cash"))
        {
            CurrentWeight -= other.gameObject.GetComponent<Rigidbody>().mass;
        }
    }
}
