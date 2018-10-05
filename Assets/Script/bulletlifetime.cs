using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletlifetime : MonoBehaviour {
    //총알 생존시간이 아니라 격발이펙트 생존시간
	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
