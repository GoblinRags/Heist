﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeItRain : MonoBehaviour
{
    public float BillSpeed = 20f;
    public GameObject BillPrefab;

    public CreateGravity CreateGrav;
    // Start is called before the first frame update
    void Start()
    {
        CreateGrav = FindObjectOfType<CreateGravity>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (CreateGrav.DropStarted)
            {
                FreeFloat();
            }
            else
            {
                ChooseDir();
            }
            
        }
    }

    public void ChooseDir()
    {

        GameObject bill = Instantiate(BillPrefab, transform.position, Quaternion.identity);
        bill.transform.parent = transform;
        Rigidbody rb = bill.GetComponent<Rigidbody>();
        rb.velocity = BillSpeed * transform.up;
        //rb.angularVelocity = Random.insideUnitSphere * 30f;
        rb.AddTorque(Random.insideUnitSphere.normalized * 100000f);
        
    }

    void FreeFloat()
    {
        GameObject bill = Instantiate(BillPrefab, transform.position, Quaternion.identity);
        bill.transform.parent = transform;
        Rigidbody rb = bill.GetComponent<Rigidbody>();
        //rb.velocity = BillSpeed * transform.up;
        //rb.angularVelocity = Random.insideUnitSphere * 30f;
        rb.AddTorque(CreateGrav.Torque);
        rb.useGravity = false;
    }
}
