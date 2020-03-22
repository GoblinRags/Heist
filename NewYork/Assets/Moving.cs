using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    public Vector3 StartPos;
    public Vector3 EndPos;
    public int Dir = 1;
    public float Lerp = 0;
    public float LerpRate = .1f;
    private void Update()
    {
        Lerp += LerpRate * Dir * Time.deltaTime;
        if (Dir == 1)
        {
            if (Lerp >= 1)
            {
                Lerp = 1;
                Dir = -1;
            }
        }
        else
        {
            
            if (Lerp <= 0)
            {
                Lerp = 0;
                Dir = 1;
            }
        }

        Move();
    }

    void Move()
    {
        transform.Translate(Vector3.Lerp(StartPos, EndPos, Lerp) - transform.position);
    }
}
