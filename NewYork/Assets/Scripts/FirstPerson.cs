using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPerson : MonoBehaviour
{
    //find a way to avoid turning all the way->clamping
    //30-55
    public float ForwardBackward;

    public float RightLeft;
    public float Jump;
    public float Speed = 1f;
    
    public float RotationSpeed = 1f;
    public CharacterController CC;

    public float MouseX;

    public float MouseY;
    public float MouseXRotSpeed = 5f;
    public float MouseYRotSpeed = 4f;
    public float MoveSpeed = 5f;
    public float JumpSpeed = 10f;
    public Vector3 InputVector;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        CC = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        ForwardBackward = Input.GetAxis("Vertical");
        RightLeft = Input.GetAxis("Horizontal");
        Jump = Input.GetAxis("Jump");

        MouseX = Input.GetAxis("Mouse X");
        MouseY = Input.GetAxis("Mouse Y");
        
        transform.Rotate(0f, MouseX * MouseXRotSpeed, 0f);
        Camera.main.transform.Rotate(-MouseY * MouseYRotSpeed, 0f, 0f);
        Debug.Log("Mouse: " + (MouseX, MouseY));
        InputVector = Vector3.zero;
        //movement
        InputVector += transform.forward * ForwardBackward;
        InputVector += transform.right * RightLeft;
        InputVector.Normalize();

        CC.Move(MoveSpeed * Time.deltaTime * InputVector + (Physics.gravity * .69f));
        CC.Move(Jump * JumpSpeed * Vector3.up);
        //take out rigidbody and capsule collider

        //child the capsule collider, make it little bigger
        //push stuff->physics layer
    }

    private void FixedUpdate()
    {
        
        //RB.velocity = MoveSpeed * Time.fixedDeltaTime * 50 * InputVector + Physics.gravity * .69f;
        //RB.AddForce(Vector3.up * Jump * JumpSpeed, ForceMode.Impulse);
    }
}
