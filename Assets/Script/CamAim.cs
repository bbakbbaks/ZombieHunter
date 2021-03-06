﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamAim : MonoBehaviour {
    float f_mouseXaxis; //마우스 x축
    public float frotateSpeed = 2.5f; //회전속도

    void Start()
    {

    }

    void Update()
    {
        if (Time.timeScale == 1)
        {
            f_mouseXaxis = Input.GetAxis("Mouse Y");
            transform.Rotate(new Vector3(-f_mouseXaxis * frotateSpeed, 0, 0));
        }
    }
}
