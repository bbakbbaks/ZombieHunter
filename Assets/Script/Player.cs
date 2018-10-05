using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public float fmoveSpeed = 5f; //이동속도
    public float fjumpPower = 5f; //점프힘
    public float f_reloadSpeed = 1; //재장전 속도
    public int n_Hp;
    public int n_MaxHp;
    public int n_Dam;    
    public float f_fireSpeedTime = 0.5f; //연사속도

    public float f_fireSpeed = 0f; //발사까지가능까지남은시간
    public float frotateSpeed = 5f; //회전속도
    float f_xaxis; //x축
    float f_zaxis; //z축
    float f_mouseYaxis; //마우스 y축
    float f_mouseXaxis; //마우스 x축
    bool b_mouseCheck = false; //커서 상태
    public GameObject Firepoint; //발사위치
    public GameObject Bullet; //총알
    public GameObject FireEffect; //발사효과
    public GameObject BulletRayObject; //레이저포인트
    public Text tbulletCountText; //총알 수 표시
    int n_bulletcount; //현재 총알 갯수
    public float f_reloadTime = 0; //재장전 시간  
    bool b_reloadCheck = false; //재장전 여부 체크
    bool b_magazine = true; //탄창이 비어있는지 체크
    public GameObject mainCam; //메인 카메라
    public GameObject fireCam; //정밀조준 카메라
    bool b_camMode = false; //카메라 모드
    bool b_fireCheck = false; //발사가능여부
    //public Enemy c_target; //적
    //bool b_targetCheck = false; //적포착여부



    Rigidbody m_RigidBody;
    Animator m_animator;

    void Awake()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        m_animator = GetComponent<Animator>();
    }

    void Start () {
        Cursor.visible = false; //커서 숨기기
        Cursor.lockState = CursorLockMode.Locked; //커서 화면안에 가두기

        n_bulletcount = 30;

        n_Hp = 100;
        n_MaxHp = n_Hp;
	}
	
	void Update () {
        Move();
        AniUpdate();
        Mouss();
        Fire();
        BulletCounttext();
        CamMode();
        FireCheck();
    }

    void FixedUpdate() //Rigidbody를 다룰때 사용
    {

    }

    void CamMode()
    {
        if (!(b_camMode))
        {
            mainCam.SetActive(true);
            fireCam.SetActive(false);
        }
        else
        {
            mainCam.SetActive(false);
            fireCam.SetActive(true);
        }
    }

    void Mouss()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            b_mouseCheck = true;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            b_mouseCheck = false;
        }
    }
    

    void Move()
    {
        f_xaxis = Input.GetAxisRaw("Horizontal");
        f_zaxis = Input.GetAxisRaw("Vertical");
        f_mouseYaxis = Input.GetAxis("Mouse X");
        f_mouseXaxis = Input.GetAxis("Mouse Y");

        transform.Translate(new Vector3(f_xaxis, 0, f_zaxis) * fmoveSpeed * Time.deltaTime);
        transform.Rotate(new Vector3(0, f_mouseYaxis * frotateSpeed, 0));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_RigidBody.AddForce(Vector3.up * fjumpPower, ForceMode.Impulse);
        }

    }

    void AniUpdate()
    {
        if (f_xaxis != 0 || f_zaxis != 0)
        {
            m_animator.SetBool("Run", true);
        }
        else
        {
            m_animator.SetBool("Run", false);
        }
    }

    void Fire()
    {
        if (Input.GetMouseButton(1))
        {
            BulletRayObject.SetActive(true);
            b_camMode = true;
        }
        else
        {
            BulletRayObject.SetActive(false);
            b_camMode = false;
        }
        if (Input.GetMouseButton(0))
        {
            if (n_bulletcount > 0 && f_reloadTime <= 0 && b_fireCheck)
            {
                b_fireCheck = false;
                //m_animator.SetBool("Shot", true);
                //m_animator.SetBool("Run", false);
                //GameObject fireBullet = Instantiate(Bullet, Firepoint.transform.position, Firepoint.transform.rotation);
                RaycastHit hit = new RaycastHit();
                if (Physics.Raycast(Firepoint.transform.position, Firepoint.transform.forward, out hit, 30f))
                {
                    if (hit.collider.CompareTag("Enemy"))
                    {
                        hit.collider.gameObject.GetComponent<Enemy>().n_Hp -= this.n_Dam;
                        hit.collider.gameObject.GetComponent<Enemy>().
                            ChangeHp(hit.collider.gameObject.GetComponent<Enemy>().n_Hp,
                            hit.collider.gameObject.GetComponent<Enemy>().n_MaxHp);
                    }
                }                
                Instantiate(FireEffect, Firepoint.transform.position, Firepoint.transform.rotation);
                SoundManager.instance.FireSound();
                n_bulletcount--;
                //m_animator.SetBool("Shot", false);

            }
            
            if (n_bulletcount == 0)
            {
                b_magazine = false;
            }
        }
        

        if (!(b_reloadCheck))
        {
            if (Input.GetKeyDown(KeyCode.R) || !(b_magazine))
            {
                SoundManager.instance.ReloadSound();
                f_reloadTime = 1f;
                b_reloadCheck = true;
                b_magazine = true;
            }
        }

        if (f_reloadTime > 0)
        {
            f_reloadTime -= (Time.deltaTime * f_reloadSpeed);
            if (f_reloadTime <= 0)
            {
                n_bulletcount = 30;
                f_reloadTime = 0;
                b_reloadCheck = false;
            }
        }
    }

    void FireCheck()
    {
        if (!(b_fireCheck))
        {           
            f_fireSpeed -= Time.deltaTime;
            if (f_fireSpeed <= 0)
            {
                f_fireSpeed = f_fireSpeedTime;
                b_fireCheck = true;
            }
        }
    }

    void BulletCounttext()
    {
        tbulletCountText.text = n_bulletcount + " / ∞";
    }
}
