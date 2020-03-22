using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperWeighted : MonoBehaviour
{
    public Weighted Wei;
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
        //Debug.Log(other.gameObject);
        if (other.gameObject.CompareTag("Player")) 
        {
            Wei.AddWeight(100);
        }
            
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Wei.AddWeight(-100);
        }
       
    }
}
