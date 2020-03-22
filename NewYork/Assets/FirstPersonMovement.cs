using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class FirstPersonMovement : MonoBehaviour
{
    public CharacterController CC;
    public Collider TriggerCol;
    public float Speed;
    
    private Vector3 Velocity;

    public float Gravity = -9.81f;
    public float JumpHeight = 3f;
    public Transform GroundCheck;

    public float GroundDist;

    public LayerMask GroundMask;
    public LayerMask CashMask;
    private bool IsGrounded;
    private bool Jumping;
    public bool IsDash;
    public bool IsDead;
    public bool Dashing;

    public float DashingTimer = 0f;
    public float DashSpeed = 15f;
    public float DashingRate = .3f;
    
    public float WalkTimer = 0f;

    public bool Walking = true;

    public float WalkTimerInterval = .5f;

    public string[] Foots;

    public int FootIndex;
    // Start is called before the first frame update
    void Start()
    {
        
        CC = GetComponent<CharacterController>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (IsDead)
        {
            
            return;
        }
        if (Walking)
        {
            if (WalkTimer >= WalkTimerInterval)
            {
                WalkTimer = 0;
                FootIndex += 1;
                if (FootIndex >= Foots.Length)
                {
                    FootIndex = 0;
                }
                string sfx = Foots[FootIndex];
                AudioManager.Instance.PlayOneShotSound(sfx, true);
            }
            else
            {
                WalkTimer += Time.deltaTime;
            }
        }
        
        
        Velocity.y += Gravity * Time.deltaTime;
        CC.Move(Velocity * Time.deltaTime);
        IsGrounded = Physics.CheckSphere(GroundCheck.position, GroundDist, GroundMask) ||
                     Physics.CheckSphere(GroundCheck.position, GroundDist, CashMask);
        bool onGround = Physics.CheckSphere(GroundCheck.position, GroundDist, GroundMask);
        bool onCash = Physics.CheckSphere(GroundCheck.position, GroundDist, CashMask);
        Debug.Log(onCash);
        if ((CC.collisionFlags & CollisionFlags.Above) != 0)
        {
            Velocity.y = -2f;
        }
        if (IsGrounded && Velocity.y < 0)
        {
            if (Jumping)
            {
                if (onCash)
                {
                    AudioManager.Instance.PlayOneShotSound("CashLand", true);
                }
                else if (onGround)
                {
                    AudioManager.Instance.PlayOneShotSound("GroundLand", true);
                }
            }

            Jumping = false;
            CC.slopeLimit = 45.0f;
            Velocity.y = -2f;
        }
        if (Input.GetButtonDown("Jump") && IsGrounded)
        {
            Jumping = true;
            CC.slopeLimit = 100.0f;
            Velocity.y = Mathf.Sqrt(JumpHeight * -2f * Gravity);
            if (onCash)
            {
                AudioManager.Instance.PlayOneShotSound("CashJump", true);
                Debug.Log("GroundJump");
            }
            else if (onGround)
            {
                AudioManager.Instance.PlayOneShotSound("GroundJump", true);
                
            }
                
        }
        if (IsDash)
        {
            if (Dashing && DashingTimer <= DashingRate)
            {
                DashingTimer += Time.deltaTime;
                float dashSpeed = DashingTimer / DashingRate * DashSpeed;
                CC.Move(dashSpeed * Time.deltaTime * transform.forward);
                return;
            }
            else
            {
                DashingTimer = 0f;
                IsDash = false;
                Dashing = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift) && !IsDash)
        {
            //IsDash = true;
            //Dashing = true;
        }    
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        if (x != 0f || z != 0f)
        {
            if (!Walking)
            {
                WalkTimer = WalkTimerInterval / 2f;
            }
            Walking = true;
            
        }
        else
        {
            Walking = false;
        }
        Vector3 move = Vector3.Normalize(transform.right * x + transform.forward * z);
        CC.Move(Speed * Time.deltaTime * move);

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Weighted"))
        {
            hit.gameObject.GetComponent<Weighted>().AddWeight(100);
        }
    }
    //private void OnControllerColliderHit
}
