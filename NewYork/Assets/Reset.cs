using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reset : MonoBehaviour
{
    public GameObject Player;
    public Vector3 Spawn;
    public Vector3 Rotation;
    public Image Canv;

    public bool Dead;
    public bool Spawned;
    public float Timer = 0f;

    public float TurnDark = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Dead)
        {
            Timer += Time.deltaTime;
            Canv.color = Color.Lerp(Color.clear, Color.black, Timer / TurnDark * 2f);
            if (Timer >= TurnDark)
            {
                if (!Spawned)
                {
                    Player.transform.localPosition = Spawn;
                    Player.transform.rotation = Quaternion.Euler(Rotation);
                    
                    Debug.Log("Test");
                    Spawned = true;
                }
                
                
                Canv.color = Color.Lerp(Color.black, Color.clear, (Timer - TurnDark) / TurnDark * 2f);
                if (Timer >= TurnDark * 2f)
                {
                    Dead = false;
                    Spawned = false;
                    Player.GetComponent<FirstPersonMovement>().IsDead = false;
                    Timer = 0f;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !Dead)
        {
            Player.GetComponent<FirstPersonMovement>().IsDead = true;
            Dead = true;
            AudioManager.Instance.PlayOneShotSound("Reset", false);
        }
    }
}
