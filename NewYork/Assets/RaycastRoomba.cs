using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastRoomba : MonoBehaviour
{

    public float MaxRayDistance = 2f;
    public float Speed = 1f;
    public bool IsTurning;

    public float RotateSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray roombaRay = new Ray(transform.position, transform.forward);
        
        
        Debug.DrawRay(roombaRay.origin, roombaRay.direction * MaxRayDistance, Color.green);

        if (Physics.Raycast(roombaRay, MaxRayDistance) && !IsTurning)
        {
            float rand = Random.Range(0f, 1f);
            if (rand < .5)
            {
                //turn right
                transform.Rotate(90f,90f, 0f);
            }
            else
            {
                //turn left
                transform.Rotate(90f,-90f, 0f);
            }

            IsTurning = true;
        }
        else
        {
            transform.Translate( 0f, 0f, Speed * Time.deltaTime);
            foreach (Transform t in GetComponentInChildren<Transform>())
            {
                t.Rotate(0f, RotateSpeed * Time.deltaTime,0f);
            }
            IsTurning = false;
        }
    }
}
