using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    public KeyCode Left = KeyCode.A;

    public KeyCode Right = KeyCode.D;

    public KeyCode Forward = KeyCode.W;

    public KeyCode Back = KeyCode.S;

    public Rigidbody RB;
    
    [FormerlySerializedAs("Speed")] public Vector2 TurnSpeed = new Vector2(2f, 2f);
    public float MovementSpeed = .7f;
    private float Yaw = 0f; //along y axis
    private float Pitch = 0f; //along x axis

    public GameObject Cam;

    public TypewriterEffect TypeWriter;
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!TypeWriter.IntroDone)
        {
            return;
        }
        
        Vector3 movement = Vector3.zero;
        if (Input.GetKey(Left))
        {
            //movement += Vector3.left;
            movement += -Cam.transform.right;
        }

        if (Input.GetKey(Right))
        {
            //movement += Vector3.right;
            movement += Cam.transform.right;
        }

        if (Input.GetKey(Forward))
        {
            //movement += Vector3.forward;
            movement += Cam.transform.forward;

        }
        
        if (Input.GetKey(Back))
        {

            //movement += Vector3.back;
            movement += -Cam.transform.forward;
        }

        

        Yaw += TurnSpeed.x * Input.GetAxis("Mouse X");
        Pitch -= TurnSpeed.y * Input.GetAxis("Mouse Y");
        
        //Mouse rotation
        transform.eulerAngles = new Vector3(Pitch, Yaw, 0f);

        RB.velocity = movement.normalized * MovementSpeed;
    }


    void ShootBill()
    {
        GameObject bill;
        //Vector3 vel = new Vector3(1, 1, 1);
    }
}
