using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    //public BulletManager.eBullet e_bullet;
    //public BulletType s_bullet;

    public float fFireSpeed = 100;
    public float flifeTime = 0.5f;
    public int n_bulletDam = 10;
    public Enemy c_target;
    public Player c_player;

	// Use this for initialization
	void Start () {
        //Destroy(this.gameObject, s_bullet.LifeTime);
        Destroy(this.gameObject, flifeTime);
        //GetComponent<Rigidbody>().AddForce(transform.forward * fFireSpeed);
        c_player = GetComponent<Player>();
        //Debug.Log(c_player.n_Dam);
    }
	
	// Update is called once per frame
	void Update () {
        //this.transform.Translate(Vector3.forward * s_bullet.BulletSpeed * Time.deltaTime);
        this.transform.Translate(Vector3.forward * fFireSpeed * Time.deltaTime);
    }

    //void OnCollisionEnter(Collision col)
    //{
    //    if (col.gameObject.tag == "Enemy")
    //    {
    //        Debug.Log(col.gameObject.tag);
    //        Destroy(this.gameObject);
    //    }
    //}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            //Debug.Log(col.gameObject.tag);
            c_target = col.gameObject.GetComponent<Enemy>();
            if (c_target.n_Hp >= 10)
            {
                //c_target.n_Hp -= s_bullet.Damage;
                c_target.n_Hp -= n_bulletDam;
                c_target.ChangeHp(c_target.n_Hp, c_target.n_MaxHp);
                Debug.Log(c_target.n_Hp);
                Destroy(this.gameObject);
            }
        }
    }
}
