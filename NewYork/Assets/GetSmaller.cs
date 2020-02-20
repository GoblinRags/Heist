using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSmaller : MonoBehaviour
{
    public MakeItRain[] Rains;
    public float OrigZ = .17f;
    public float NewZ = .05f;
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
        if (other.gameObject.CompareTag("Ball"))
        {
            other.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0f, 1.5f, 0f);
            Vector3 scale = transform.localScale;
            transform.localScale = new Vector3(scale.x, scale.y, NewZ);
            scale = other.gameObject.transform.localScale;
            other.gameObject.transform.localScale = new Vector3(scale.x, scale.y, scale.z / 3f);
            
            foreach (MakeItRain rain in Rains)
            {
                rain.ChooseDir();
            }
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            //other.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0f, 1f, 0f);
            Vector3 scale = other.gameObject.transform.localScale;
            other.gameObject.transform.localScale = new Vector3(scale.x, scale.y, scale.z / 3f);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            //other.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0f, 1f, 0f);
            Vector3 scale = transform.localScale;
            transform.localScale = new Vector3(scale.x, scale.y, OrigZ);
            scale = other.gameObject.transform.localScale;
            other.gameObject.transform.localScale = new Vector3(scale.x, scale.y, .043f);
        }
    }
}
