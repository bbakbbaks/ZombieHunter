using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour {
    float f_mouseXaxis; //마우스 x축
    public float frotateSpeed = 2.5f; //회전속도
    public Player c_player;

    void Start () {
		
	}

	void Update () {
        if (Time.timeScale == 1)
        {
            if (!(c_player.b_camMode))
            {
                f_mouseXaxis = Input.GetAxis("Mouse Y");
                transform.Rotate(new Vector3(0, -f_mouseXaxis * frotateSpeed, 0));
            }
        }
    }
}
