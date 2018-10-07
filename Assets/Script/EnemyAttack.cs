using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    public Enemy c_Zombie;
    public Player c_target;
    float f_attacktime = 1;
    int n_hitDam;

    //void Awake()
    //{
    //    c_Zombie = GetComponent<Enemy>();
    //}

    void Update()
    {
        if (f_attacktime > 0)
        {
            f_attacktime -= Time.deltaTime;
        }        
        else
        {
            f_attacktime = 0;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            c_target = col.gameObject.GetComponent<Player>();
            n_hitDam = c_Zombie.n_dam - c_target.n_Armor;
            if (n_hitDam <= 0)
            {
                n_hitDam = 1;
            }

            if (f_attacktime <= 0)
            {
                c_target.n_Hp -= n_hitDam;
                c_target.ChangeHp(c_target.n_Hp, c_target.n_MaxHp);
                //Debug.Log(c_target.n_Hp);
                //Debug.Log(c_target.n_MaxHp);
                f_attacktime = 1;
            }
        }
    }
}
