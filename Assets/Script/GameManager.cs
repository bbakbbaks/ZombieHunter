using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public BulletManager c_bulletManager;

    static GameManager m_cInstance;

    static public GameManager GetInstance()
    {
        return m_cInstance;
    }
    // Use this for initialization
    void Start () {
        m_cInstance = this;
        CreateBullet();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CreateBullet()
    {
        c_bulletManager.m_bulletList.Add(new BulletType("일반탄", 10, 0.5f, 100));
        c_bulletManager.m_bulletList.Add(new BulletType("파워탄", 15, 0.5f, 100));
        c_bulletManager.m_bulletList.Add(new BulletType("특수탄", 20, 0.5f, 100));
    }
}
