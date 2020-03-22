using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickTo : MonoBehaviour
{
    private Rigidbody RB;
    private AudioSource AS;

    //public GameObject Collided;
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
        AS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        
        if (!RB.isKinematic && !other.gameObject.CompareTag("Player"))
        {
            AS.Play();
            FixedJoint joint = gameObject.AddComponent<FixedJoint>();
            joint.connectedBody = other.gameObject.GetComponent<Rigidbody>();
            GetComponent<Rigidbody>().isKinematic = true;
            if (other.gameObject.GetComponent<Moving>() || other.gameObject.CompareTag("Cash"))
            {
                //gameObject.transform.parent = other.gameObject.transform;
            }
            
        }
    }
}
