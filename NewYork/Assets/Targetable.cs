using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetable : MonoBehaviour
{
    public int Health = 1;

    public Material ChangeMaterial;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Cash"))
        {
            //turn object green :)
            Health -= 1;
            if (Health <= 0)
            {
                //Destroy(gameObject);
                GetComponent<MeshRenderer>().material = ChangeMaterial;

            }
            //create explosion hit and sfx
            //destroy object
            Destroy(other.gameObject);
        }
    }
}
