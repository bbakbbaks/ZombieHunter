using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour {
    float f_mouseXaxis; //마우스 x축
    public float frotateSpeed = 2.5f; //회전속도

    void Start () {
		
	}

	void Update () {
        f_mouseXaxis = Input.GetAxis("Mouse Y");
        transform.Rotate(new Vector3(0, -f_mouseXaxis * frotateSpeed, 0));
    }
}
