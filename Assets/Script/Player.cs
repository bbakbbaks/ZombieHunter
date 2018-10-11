using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public float fmoveSpeed = 5f; //이동속도
    public float fjumpPower = 5f; //점프힘
    public float f_reloadSpeed = 1; //재장전 속도
    public float n_Hp = 100;
    public float n_MaxHp = 100;
    public float n_Dam = 10;
    public float n_Armor = 0;
    public float f_fireSpeedTime = 0.5f; //연사속도
    public float f_kitTime = 30; //키트 재사용 시간
    float f_kitCoolTime = 0; //키트 시간 감소
    bool b_useKit = false; //키트 사용 여부
    public int n_Lv = 1;
    public int n_exp = 0;
    public int n_expMax = 100;
    public int n_bonusStat = 0;
    bool b_StatusWindowCheck = false; //스텟창 오픈여부
    public bool b_MapCheck = false; //맵 오픈 여부

    public GameObject StatusWindow; //스텟창    
    public GameObject Firepoint; //발사위치
    public GameObject Bullet; //총알
    public GameObject FireEffect; //발사효과
    public GameObject BulletRayObject; //레이저포인트
    public GameObject hitEffect; //타격효과
    public GameObject mainCam; //메인 카메라
    public GameObject fireCam; //정밀조준 카메라
    public GameObject MinMap; //Mini맵
    public GameObject Map; //맵
    public GameObject Mark; //마크

    public HpBar m_hpbar;
    float f_max; //Hpbar의 크기
    public Kitbar m_kitbar;
    public GameObject g_kitBar; //키트 블록창
    float f_kitMax; //kitbar 크기
    public HpBar m_expBar;
    float f_expMax;

    public float f_fireSpeed = 0f; //발사까지가능까지남은시간
    public float frotateSpeed = 5f; //회전속도
    float f_xaxis; //x축
    float f_zaxis; //z축
    float f_mouseYaxis; //마우스 y축
    float f_mouseXaxis; //마우스 x축
    bool b_mouseCheck = false; //커서 상태    
    int n_bulletcount = 30; //현재 총알 갯수
    public float f_reloadTime = 0; //재장전 시간  
    bool b_reloadCheck = false; //재장전 여부 체크
    bool b_magazine = true; //탄창이 비어있는지 체크
    bool b_camMode = false; //카메라 모드
    bool b_fireCheck = false; //발사가능여부

    public Text t_kitCoolTimeText; //키트 쿨타임 표시
    public Text tbulletCountText; //총알 수 표시
    public Text t_hpText;
    public Text t_damegeText;
    public Text t_reloadText;
    public Text t_armorText;
    public Text t_moveSpeedText;
    public Text t_fireSpeedText;
    public Text t_kitCoolTimeText_Status;
    public Text t_bonusText;
    public Text t_expText;
    public Text t_LvText;

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
        f_max = m_hpbar.m_cRectTransform.sizeDelta.x;
        f_kitMax = m_kitbar.m_cRectTransform.sizeDelta.y;
        f_expMax = m_expBar.m_cRectTransform.sizeDelta.x;
        Cursor.visible = false; //커서 숨기기
        Cursor.lockState = CursorLockMode.Locked; //커서 화면안에 가두기
        ChangeExp(n_exp, n_expMax);
	}

    public void ChangeHp(float unithp, float unitmaxhp)//HP바의 체력변화
    {
        float HpRatio = unithp / unitmaxhp * f_max;
        m_hpbar.m_cRectTransform.sizeDelta = new Vector3(HpRatio, m_hpbar.m_cRectTransform.sizeDelta.y);
    }

    public void ChangeExp(float exp, float maxexp)//HP바의 체력변화
    {
        float ExpRatio = exp / maxexp * f_expMax;
        m_expBar.m_cRectTransform.sizeDelta = new Vector3(ExpRatio, m_expBar.m_cRectTransform.sizeDelta.y);
    }

    void ChangeKit(float kitcooltime, float kitcooltimemax)//HP바의 체력변화
    {
        float kitRatio = kitcooltime / kitcooltimemax * f_kitMax;
        m_kitbar.m_cRectTransform.sizeDelta = new Vector3(m_kitbar.m_cRectTransform.sizeDelta.x, kitRatio);
    }

    void Update () {
        PlayerUI();
        if (Time.timeScale == 1)
        {
            Move();
            AniUpdate();
            Fire();
            
            CamMode();
            FireCheck();
            firstAidKit();
            LvUp();
        }
        UItext();
    }

    void FixedUpdate() //Rigidbody를 다룰때 사용
    {

    }

    void PlayerUI()
    {
        Mark.transform.position = new Vector3(this.transform.position.x, -0.3f, this.transform.position.z);
        
        if (!(b_StatusWindowCheck))
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                b_mouseCheck = true;
                StatusWindow.SetActive(true);
                b_StatusWindowCheck = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                Time.timeScale = 1;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                b_mouseCheck = false;
                StatusWindow.SetActive(false);
                b_StatusWindowCheck = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            b_mouseCheck = true;
        }

        if (!(b_MapCheck))
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                //Time.timeScale = 0;
                Map.SetActive(true);
                b_MapCheck = true;
                MinMap.SetActive(false);
                //MapCam.transform.position = new Vector3(this.transform.position.x, -150, this.transform.position.z);
            }            
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                //Time.timeScale = 1;
                Map.SetActive(false);
                b_MapCheck = false;
                MinMap.SetActive(true);
                //MapCam.transform.position = new Vector3(this.transform.position.x, -50, this.transform.position.z);
            }            
        }
        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    Cursor.visible = false;
        //    Cursor.lockState = CursorLockMode.Locked;
        //    b_mouseCheck = false;
        //}
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

    void firstAidKit()
    {
        if (!(b_useKit))
        {
            t_kitCoolTimeText.text = "";
            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                n_Hp += (n_MaxHp / 2);
                if (n_Hp > n_MaxHp)
                {
                    n_Hp = n_MaxHp;
                }
                ChangeHp(n_Hp, n_MaxHp);
                f_kitCoolTime = f_kitTime;
                b_useKit = true;
            }
        }
        else
        {
            f_kitCoolTime -= Time.deltaTime;           
            t_kitCoolTimeText.text = (int)f_kitCoolTime + "";
            if (f_kitCoolTime <= 0)
            {
                f_kitCoolTime = 0;
                b_useKit = false;              
            }
        }

        if (f_kitCoolTime <= 0)
        {
            g_kitBar.SetActive(false);
        }
        else
        {
            g_kitBar.SetActive(true);
            ChangeKit(f_kitCoolTime, f_kitTime);           
        }
    }

    void Move()
    {
        f_xaxis = Input.GetAxisRaw("MyHorizontal");
        f_zaxis = Input.GetAxisRaw("MyVertical");
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
            if (n_bulletcount == 0)
            {
                b_magazine = false;
            }

            if (n_bulletcount >= 1 && f_reloadTime <= 0 && b_fireCheck)
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
                        if (hit.collider.gameObject.GetComponent<Enemy>().n_Hp > 0)
                        {
                            hit.collider.gameObject.GetComponent<Enemy>().n_Hp -= this.n_Dam;
                            Instantiate(hitEffect, hit.collider.gameObject.GetComponent<Enemy>().
                                shotEffect.transform.position, Quaternion.identity);
                            hit.collider.gameObject.GetComponent<Enemy>().
                                ChangeHp(hit.collider.gameObject.GetComponent<Enemy>().n_Hp,
                                hit.collider.gameObject.GetComponent<Enemy>().n_MaxHp);
                            hit.collider.gameObject.GetComponent<Enemy>().b_shotCheck = true;
                            if (hit.collider.gameObject.GetComponent<Enemy>().n_Hp <= 0)
                            {
                                n_exp += hit.collider.gameObject.GetComponent<Enemy>().n_giveExp;
                                ChangeExp(n_exp, n_expMax);
                                Debug.Log(n_exp);
                            }
                        }
                    }
                }                
                Instantiate(FireEffect, Firepoint.transform.position, Firepoint.transform.rotation);
                SoundManager.instance.FireSound();
                n_bulletcount--;
                //m_animator.SetBool("Shot", false);

            }            
        }
        

        if (!(b_reloadCheck))
        {
            if (Input.GetKeyDown(KeyCode.R) || !(b_magazine))
            {
                b_magazine = true;
                SoundManager.instance.ReloadSound();
                f_reloadTime = f_reloadSpeed;
                b_reloadCheck = true;               
            }
        }

        if (f_reloadTime > 0)
        {
            f_reloadTime -= Time.deltaTime;
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

    public void LvUp()
    {
        if (n_exp >= n_expMax)
        {
            n_exp -= n_expMax;
            n_Lv++;
            n_expMax = 100 * n_Lv;
            n_bonusStat++;
            ChangeExp(n_exp, n_expMax);
        }
    }

    void UItext()
    {
        tbulletCountText.text = n_bulletcount + " / ∞"; //총알 감소 텍스트
        t_hpText.text = "HP: " + (int)n_Hp + " / " + (int)n_MaxHp;
        t_expText.text = "EXP: " + (int)n_exp + " / " + (int)n_expMax;
        t_damegeText.text = "Damege: " + (int)n_Dam;        
        if (f_reloadSpeed > 0.1)
        {           
            t_reloadText.text = string.Format("Reload: {0:0.#} S", f_reloadSpeed);
        }
        else
        {           
            t_reloadText.text = string.Format("Reload: {0:0.#} S (MAX)", f_reloadSpeed);
        }
        t_armorText.text = string.Format("Armor: {0:0.#}", n_Armor);
        t_moveSpeedText.text = string.Format("MoveSpeed: {0:0.#}", fmoveSpeed);
        if (f_fireSpeedTime > 0.1)
        {
            t_fireSpeedText.text = string.Format("FireSpeed: {0:0.###} S", f_fireSpeedTime);
        }
        else
        {
            t_fireSpeedText.text = string.Format("FireSpeed: {0:0.###} S (MAX)", f_fireSpeedTime);
        }

        if (f_kitTime > 10)
        {
            t_kitCoolTimeText_Status.text = "KitCoolTime: " + (int)f_kitTime + " S";
        }
        else
        {
            t_kitCoolTimeText_Status.text = "KitCoolTime: " + (int)f_kitTime + " S (MAX)";
        }
        t_bonusText.text = "BonusPoint: " + (int)n_bonusStat;
        t_LvText.text = "Lv: " + n_Lv;
    }

    public void HpPlusButton()
    {
        if (n_bonusStat > 0)
        {
            n_bonusStat--;
            n_MaxHp += 50;
            n_Hp += 50;
        }
        ChangeHp(n_Hp, n_MaxHp);
    }

    public void DamPlusButton()
    {
        if (n_bonusStat > 0)
        {
            n_bonusStat--;
            n_Dam++;
        }
    }

    public void ArmorPlusButton()
    {
        if (n_bonusStat > 0)
        {
            n_bonusStat--;
            n_Armor += 0.2f;
        }
    }

    public void ReloadPlusButton()
    {
        if (n_bonusStat > 0 && f_reloadSpeed > 0.1)
        {
            n_bonusStat--;
            f_reloadSpeed -= 0.1f;
        }
    }

    public void MoveSpeedPlusButton()
    {
        if (n_bonusStat > 0)
        {
            n_bonusStat--;
            fmoveSpeed += 0.2f;
        }
    }

    public void FireSpeedPlusButton()
    {
        if (n_bonusStat > 0 && f_fireSpeedTime > 0.1)
        {
            n_bonusStat--;
            f_fireSpeedTime -= 0.025f;
        }
    }

    public void KitCoolTimePlusButton()
    {
        if (n_bonusStat > 0 && f_kitTime > 10)
        {
            n_bonusStat--;
            f_kitTime--;
        }
    }
}
