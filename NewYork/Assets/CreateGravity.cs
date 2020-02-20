using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CreateGravity : MonoBehaviour
{
    public List<GameObject> ObjToAddGravity = new List<GameObject>();

    public GameObject Diamond;
    public GameObject DirLight;
    public GameObject Camera1Obj;
    [FormerlySerializedAs("CameraObj")] public GameObject Camera2Obj;
    public GameObject Camera3Obj;
    public GameObject CashOne;
    public bool DropStarted;
    public Vector3 Torque = new Vector3(100f, 100f, 50f);

    public float Timer;

    public bool StartTimer;

    public GameObject Ball;
    public Rigidbody BallRB;
    
    public TypewriterEffect TypeWriter;

    public bool FloorFallen;

    private PlayerLogic2 PL2;

    public bool Teleported;

    public GameObject ParticlePrefab;

    public bool Done;
    // Start is called before the first frame update
    void Start()
    {
        TypeWriter = FindObjectOfType<TypewriterEffect>();
        BallRB = Ball.GetComponent<Rigidbody>();
        PL2 = FindObjectOfType<PlayerLogic2>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!TypeWriter.IntroDone)
        {
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartTimer = true;
            BallRB.useGravity = true;
        }

        if (StartTimer)
        {
            Timer += Time.deltaTime;
        }

        if (Timer >= 43f && !Done)
        {
            PL2.GameDone();
            Done = true;
        }
        else if (Timer >= 41f && !Teleported)
        {
            Diamond.GetComponent<MeshRenderer>().enabled = false;
            GameObject dia = Instantiate(ParticlePrefab, Diamond.transform.position, Quaternion.identity);
            dia.transform.parent = Diamond.transform;
            Teleported = true;
        }
        else if (Timer >= 21.2f)
        {
            Camera1Obj.SetActive(false);
            Camera2Obj.SetActive(true);
        }
        else if (Timer >= 20f & !FloorFallen)
        {
            
            AddGravity();
            DiamondGravity();
            Camera();
            DropStarted = true;
            DirLight.SetActive(true);
            FloorFallen = true;
        }
        else if (Timer >= 16f & !FloorFallen)
        {
            
            Camera1Obj.SetActive(true);
            Camera3Obj.SetActive(false);
        }
        else if (Timer >= 8f & !FloorFallen)
        {
            Camera1Obj.SetActive(false);
            Camera3Obj.SetActive(true);
            
        }
        
        
        if (Input.GetKeyDown(KeyCode.B))
        {
            //CashOne.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 3f);
            //CashOne.GetComponent<Rigidbody>().AddTorque(0f, 0f, 100000f, ForceMode.Impulse);
        }
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    AddGravity();
        //    DiamondGravity();
        //    Camera();
        //    DropStarted = true;
        //    DirLight.SetActive(true);
        //    Camera1Obj.SetActive(false);
        //    Camera2Obj.SetActive(true);
        //}
    }

    void AddGravity()
    {
        foreach (GameObject obj in ObjToAddGravity)
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = obj.AddComponent<Rigidbody>();
            }
            rb.AddTorque(Torque);
        }
    }


    void DiamondGravity()
    {
        Diamond.SetActive(true);
        Rigidbody rb = Diamond.GetComponent<Rigidbody>();
        rb.AddTorque(Torque);
        Camera2Obj.transform.parent = Diamond.transform;
    }

    void Camera()
    {
        //Rigidbody rb = CameraObj.AddComponent<Rigidbody>();
        //rb.AddTorque(Torque);
        //rb.useGravity = false;
    }
}
