using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    public int n_Hp = 50;
    public int n_MaxHp = 50;
    public float f_movespeed = 3.5f;
    public float f_walkSpeed = 1.5f;
    float deadtime = 1.2f; //사라지는 시간
    float f_dist; //기존위치와 좀비 위치와의 거리. 

    public Player c_target;
    public HpBar m_Hpbar;
    float f_max; //Hpbar의 크기
    bool b_return = false; //기존위치로 되돌아갈지 여부
    bool b_randomMove = true; //타겟이 없을경우 주변 랜덤 이동
    bool b_resetRandomLocation = true; //랜덤좌표 도달시 새로운 좌표 생성용

    NavMeshAgent nav;
    Vector3 selfposition; //처음위치 저장용
    Vector3 targetposition; //이동할 위치
    Vector3 randomPoition; //랜덤 좌표

    Rigidbody m_RigidBody;
    Animator m_animator;

    void Awake()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        m_animator = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
    }

    void Start () {
        //n_Hp = 50;
        //n_MaxHp=n_Hp;
        f_max = m_Hpbar.m_cRectTransform.sizeDelta.x;
        selfposition = this.transform.position;
        targetposition = selfposition;
    }

    public void ChangeHp(float unithp, float unitmaxhp)//HP바의 체력변화
    {
        float HpRatio = unithp / unitmaxhp * f_max;
        m_Hpbar.m_cRectTransform.sizeDelta = new Vector3(HpRatio, m_Hpbar.m_cRectTransform.sizeDelta.y);
    }

    // Update is called once per frame
    void Update () {
        Dead();
        EnemyAiDetect();
        if (n_Hp > 0)
        {
            nav.SetDestination(targetposition);
        }
	}

    void Dead()
    {
        if (n_Hp <= 0)
        {
            this.targetposition = this.transform.position;
            m_animator.SetBool("Death", true);            
            deadtime -= Time.deltaTime;
            if (deadtime <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    void EnemyAiDetect()
    {
        if (!(b_return))
        {
            if (c_target == null)
            {
                if (b_randomMove)
                {
                    if (b_resetRandomLocation)
                    {
                        randomPoition.x = Random.Range(selfposition.x - 5, selfposition.x + 5);
                        randomPoition.z = Random.Range(selfposition.z - 5, selfposition.z + 5);
                        b_resetRandomLocation = false;
                    }
                    this.targetposition = randomPoition;
                    m_animator.SetBool("Walk", true);
                    nav.speed = f_walkSpeed;
                    f_dist = Vector3.Distance(this.transform.position, randomPoition);
                    if (f_dist <= 1)
                    {
                        b_resetRandomLocation = true; //랜덤좌표에 도착
                    }
                }


                Collider[] hitCollider = Physics.OverlapSphere(this.transform.position, 15.0f);

                foreach (Collider hit in hitCollider)
                {
                    if (hit.CompareTag("Player"))
                    {
                        c_target = hit.gameObject.GetComponent<Player>();
                        m_animator.SetBool("Walk", false);
                        b_randomMove = false;
                    }
                }
            }

            if (c_target != null)
            {
                this.targetposition = c_target.transform.position;
                m_animator.SetBool("Run", true);
                nav.speed = f_movespeed;

                f_dist = Vector3.Distance(this.transform.position, selfposition);
                if (f_dist >= 30)
                {
                    b_return = true;
                    c_target = null;
                    m_animator.SetBool("Run", false);
                }
            }
        }
        else
        {
            this.targetposition = selfposition;
            m_animator.SetBool("Run", true);
            nav.speed = f_movespeed;
            f_dist = Vector3.Distance(this.transform.position, selfposition);
            if (f_dist <= 1.5)
            {
                m_animator.SetBool("Run", false);
                m_animator.SetBool("Walk", true);
                nav.speed = f_walkSpeed;
                b_return = false;
                b_randomMove = true;
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag != "Player")
        {
            b_resetRandomLocation = true;
        }
    }
}
