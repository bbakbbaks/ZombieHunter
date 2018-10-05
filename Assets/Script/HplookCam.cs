using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HplookCam : MonoBehaviour {
    Camera cameralook;
    //Hpbar가 카메라를 바라보게 만드는 스크립트
    void Start()
    {
        cameralook = Camera.main;
    }

    void Update()
    {
        Vector3 v = cameralook.transform.position - transform.position;
        v.x = v.z = 0;
        transform.LookAt(cameralook.transform.position - v);
    }
}
