using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public CharacterController CC;
    
    public float Speed;
    
    private Vector3 Velocity;

    public float Gravity = -9.81f;
    public float JumpHeight = 3f;
    public Transform GroundCheck;

    public float GroundDist;

    public LayerMask GroundMask;
    

    private bool IsGrounded;

    public bool IsDash;

    public bool Dashing;

    public float DashingTimer = 0f;
    public float DashSpeed = 15f;
    public float DashingRate = .3f;
    // Start is called before the first frame update
    void Start()
    {
        
        CC = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Velocity.y += Gravity * Time.deltaTime;
        CC.Move(Velocity * Time.deltaTime);
        IsGrounded = Physics.CheckSphere(GroundCheck.position, GroundDist, GroundMask);
        if ((CC.collisionFlags & CollisionFlags.Above) != 0)
        {
            Velocity.y = -2f;
        }
        if (IsGrounded && Velocity.y < 0)
        {
            CC.slopeLimit = 45.0f;
            Velocity.y = -2f;
        }
        if (Input.GetButtonDown("Jump") && IsGrounded)
        {
            CC.slopeLimit = 100.0f;
            Velocity.y = Mathf.Sqrt(JumpHeight * -2f * Gravity);
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
            IsDash = true;
            Dashing = true;
        }    
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = Vector3.Normalize(transform.right * x + transform.forward * z);
        CC.Move(Speed * Time.deltaTime * move);

    }
}
