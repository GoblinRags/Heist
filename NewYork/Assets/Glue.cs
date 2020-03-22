using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glue : MonoBehaviour
{
    public GameObject GlueBill;
    public ShootMoney ShootMo;
    public Collider Col;
    // Start is called before the first frame update
    void Start()
    {
        ShootMo = FindObjectOfType<ShootMoney>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Col.isTrigger = false;
            ShootMo.BillPrefab = GlueBill;
            ShootMo.BillSpeed = 50f;
            Destroy(gameObject);
        }
    }
}
