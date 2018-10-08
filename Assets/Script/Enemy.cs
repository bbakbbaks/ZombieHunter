using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    public float n_Hp = 50;
    public float n_MaxHp = 50;
    public float f_movespeed = 4f;
    public float f_walkSpeed = 1f;
    public float n_dam = 5; 
    float deadtime = 1.2f; //사라지는 시간
    float f_dist; //기존위치와 좀비 위치와의 거리. 
    float f_targetDist; //좀비와 플레이어간의 거리
    float looktime = 1; //재자리 공격시 바라보게 만들기위한 시간
    public int n_giveExp = 10; //사망시 주는 경험치
    public GameObject shotEffect; //피격 이펙트

    public Player c_target; //플레이어를 인식
    public HpBar m_Hpbar;
    float f_max; //Hpbar의 크기
    bool b_return = false; //기존위치로 되돌아갈지 여부
    bool b_randomMove = true; //타겟이 없을경우 주변 랜덤 이동
    bool b_resetRandomLocation = true; //랜덤좌표 도달시 새로운 좌표 생성용
    bool b_attack = false; //공격여부
    bool b_attackfinish = false;
    public bool b_shotCheck = false; //피격체크
    float f_stoptime = 0.5f; //경직시간

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
        ShotCheck();
	}

    void ShotCheck()
    {
        if (b_shotCheck)
        {
            f_stoptime -= Time.deltaTime;
            b_shotCheck = false;
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
                if (!(b_shotCheck))
                {
                    this.targetposition = c_target.transform.position;
                    m_animator.SetBool("Run", true);
                }
                else
                {
                    this.targetposition = this.transform.position;
                    m_animator.SetBool("Run", false);
                }
                //this.targetposition = c_target.transform.position;
                //m_animator.SetBool("Run", true);
                nav.speed = f_movespeed;
                f_targetDist = Vector3.Distance(this.transform.position, c_target.transform.position);
                if (f_targetDist <= 1.4)
                {
                    this.targetposition = this.transform.position;
                    looktime -= Time.deltaTime;
                    if (looktime <= 0)
                    {
                        this.transform.LookAt(c_target.transform);
                        looktime = 1;
                    }
                    m_animator.SetBool("Attack", true);

                    //float looktime = 0;
                    //if (b_attackfinish)
                    //{
                    //    looktime -= Time.deltaTime;
                    //    if (looktime <= 0)
                    //    {
                    //        b_attackfinish = false;
                    //        looktime = 1;
                    //    }
                    //}

                    //if (!(b_attackfinish))
                    //{
                    //    b_attack = true;
                    //    if (b_attack)
                    //    {
                    //        this.targetposition = this.transform.position;
                    //        m_animator.SetBool("Attack", true);
                    //        b_attackfinish = true;
                    //    }
                    //}
                }
                else
                {
                    if (!(b_shotCheck))
                    {
                        this.targetposition = c_target.transform.position;
                    }
                    m_animator.SetBool("Attack", false);
                }

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

    //void AttackMode()
    //{
    //    if (b_attack)
    //    {
    //        this.targetposition = this.transform.position;
    //        m_animator.SetBool("Attack", true);
    //        b_attackfinish = true;
    //    }
    //    else
    //    {
    //        this.targetposition = c_target.transform.position;
    //        m_animator.SetBool("Attack", false);
    //    }
    //}
}
