using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weighted : MonoBehaviour
{
    public float CurrentWeight;
    public float WeightSupported;
    public float WeightTilt;
    public Collider TriggerCol;
    public Rigidbody RB;
    public bool Fallen = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Player")) 
        {
            CurrentWeight += 100;
        }
        if (CurrentWeight >= WeightSupported && GetComponent<Rigidbody>() == null)
        {
            AddRB();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            CurrentWeight -= 100;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        
        //Debug.Log(other.gameObject);
        bool checkWeight;
        if (other.gameObject.CompareTag("Cash"))
        {
            checkWeight = true;
            CurrentWeight += other.gameObject.GetComponent<Rigidbody>().mass;
            

            
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            CurrentWeight += 100;
            //Debug.Log("player");
            //CurrentWeight += other.gameObject.GetComponent<CharacterController>().mass;
        }
        if (CurrentWeight >= WeightSupported  && GetComponent<Rigidbody>() == null)
        {
            AddRB();
        }
        
    }
    
    


    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Cash"))
        {
            CurrentWeight -= other.gameObject.GetComponent<Rigidbody>().mass;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            CurrentWeight -= 100;
        }
    }

    public void AddWeight(int mass)
    {
        CurrentWeight += mass;
        //Debug.Log("used");
        if (CurrentWeight >= WeightSupported && GetComponent<Rigidbody>() == null)
        {
            AddRB();
        }
    }

    public void AddRB()
    {
        RB = gameObject.AddComponent<Rigidbody>();
        RB.mass = 2;
        
        Fallen = true;
    }
}
