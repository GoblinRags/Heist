using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandBoxDone : MonoBehaviour
{

    public Targetable[] Targets;

    public bool Victory = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool gameDone = true;
        foreach (var target in Targets)
        {
            if (target.Health > 0)
            {
                gameDone = false;
            }
        }
        if (gameDone && !Victory)
        {
            FindObjectOfType<SandboxLogic>().Victory();
            Victory = true;
        }
    }
    
    
}
