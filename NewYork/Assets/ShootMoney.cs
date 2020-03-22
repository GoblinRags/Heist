using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShootMoney : MonoBehaviour
{
    public TextMeshProUGUI CashUI;
    public int CashAmount = 0;
    public float BillSpeed = 20f;
    public float BillRaySpeed = 5f;
    public float BPS = 3f;
    public float BulletTimer = 0f;
    public GameObject BillPrefab;
    public float NormalZoom = 60f;
    public float ZoomIn = 40f;
    public Vector3 ZoomPos = new Vector3(0f, -0.2f, .87f);
    public Vector3 ZoomRotation = new Vector3(-84f, 180f, 0f);
    public Vector3 NormalPos = new Vector3(0.0182f, -0.01f, .06f);
    public Vector3 NormalRotation = new Vector3(-79.6f, 180f, -7.7f);
    public bool CanShoot;
    //public Camera Cam;
    public Transform CashGun;
    public GameObject Spawner;

    public GameObject RotationSpawner;

    public RaycastGun RayGun;
    //public GameObject Sa
    // Start is called before the first frame update
    void Start()
    {
        RayGun = FindObjectOfType<RaycastGun>();
        CashGun = GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        BulletTimer += Time.deltaTime;
        CanShoot = false;
        if (BulletTimer >= 1.0f / BPS)
        {
            CanShoot = true;
        }
        if (Input.GetButton("Fire1"))
        {
            if (CanShoot)
            {
                Shoot();
                
                BulletTimer = 0f;
            }
        }
        
        if (Input.GetButton("Fire2"))
        {
            Camera.main.fieldOfView = ZoomIn;
            CashGun.transform.localPosition = ZoomPos;
            CashGun.transform.localRotation = Quaternion.Euler(ZoomRotation);
            
        }
        else
        {
            Camera.main.fieldOfView = NormalZoom;
            CashGun.transform.localPosition = NormalPos;
            CashGun.transform.localRotation = Quaternion.Euler(NormalRotation);
        }
    }

    public void Shoot()
    {
        GameObject bill = Instantiate(BillPrefab, Spawner.transform.position, Quaternion.Euler(0f,90f, 0f));
        //bill.transform.parent = transform;
        Rigidbody rb = bill.GetComponent<Rigidbody>();
        if (RayGun.RaycastedObj != Vector3.zero)
        {
            Vector3 dir = (RayGun.RaycastedObj - Spawner.transform.position).normalized;
            rb.velocity = dir * BillRaySpeed;
            if (rb.velocity.magnitude < BillSpeed)
            {
                rb.velocity = BillSpeed * Spawner.transform.up;
            }

            bill.transform.localPosition += dir.normalized/2f;
        }
        else
        {
            rb.velocity = BillSpeed * Spawner.transform.up;
            //rb.velocity = BillSpeed * Camera.main.ScreenPointToRay(Input.mousePosition);
            bill.transform.localPosition += rb.velocity.normalized * .3f;
            bill.transform.localPosition += rb.velocity.normalized/2f;
        }
        CashAmount += 10000;
        //increase ui
        CashUI.enabled = true;

        DisplayCash(CashAmount);
        //CashUI.text = 
        //rb.angularVelocity = Random.insideUnitSphere * 30f;
        //rb.AddTorque(Random.insideUnitSphere.normalized * 100000f);

    }

    void DisplayCash(int cash)
    {
        string cashString = cash.ToString();
        string newString = cashString;
        int places = 0;
        for (int i = cashString.Length - 1; i >= 0; i--)
        {
            if (places == 3)
            {
                newString = newString.Insert(i + 1, ",");
                places = 0;
            }
            places += 1;
        }

        newString = newString.Insert(0, "$");
        CashUI.text = newString;
    }
    
}