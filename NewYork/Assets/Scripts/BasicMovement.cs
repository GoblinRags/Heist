using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicMovement : MonoBehaviour
{
    public GameObject MoveObject;

    public float MovementSpeed = 1f;

    public bool KeyPickedUp = false;

    public bool VaultOpened = false;
    public bool MoneyTaken = false;

    public bool DoorOpened = false;

    public bool AirplaneTaken = false;
    public Text CanvasText; 

    public String PhoneString = "You were able to break into Mike's room, nice!\n\nNow time to break into the vault!\n\n" +
                                "look for the key, it should be at the top of the floating money";

    void Start()
    {
        CanvasText.text = PhoneString;
    }
    // Update is called once per frame
    void Update()
    {
        Move();

    }

    void Move()
    {
        Vector3 movement = Vector3.zero;
        Vector3 rotation = Vector3.zero;
        // if w or s is pressed, i move the object around
        //move forward
        if (Input.GetKey(KeyCode.W))
        {
            //transform.Translate(new Vector3(0, 0, MovementSpeed));

            movement.z += MovementSpeed;
        }
        //move backward
        if (Input.GetKey(KeyCode.S))
        {
            //transform.Translate(new Vector3(0, 0, MovementSpeed));
            movement.z -= MovementSpeed;
        }
        transform.Translate(movement);
        
        //if A or D, rotate object y axis
        //rotate
        if (Input.GetKey(KeyCode.A))
        {
            rotation.y += 1f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            rotation.y -= 1f;
        }
        
        transform.Rotate(rotation);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("VaultKey"))
        {
            
        }
        else if (other.gameObject.CompareTag("Vault"))
        {
            
        }
        else if (other.gameObject.CompareTag("Door"))
        {
            
        }
        else if (other.gameObject.CompareTag("Vault"))
        {
            
        }
    }

    void CheckItems()
    {
        
    }
}
