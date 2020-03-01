using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private PlayerLogic2 PlayerLog2;
    // Start is called before the first frame update
    void Start()
    {
        PlayerLog2 = FindObjectOfType<PlayerLogic2>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Airplane"))
        {
            PlayerLog2.GameDone();
            Destroy(gameObject);
        }
    }
}
