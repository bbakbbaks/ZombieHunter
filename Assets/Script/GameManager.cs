using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public BulletManager c_bulletManager;
    public Exit c_exit;
    public Player c_player;

    public GameObject tutorialMove;
    public GameObject tutorialFire;
    public GameObject tutorialUiWindow;
    public GameObject tutorialBullet;
    public GameObject tutorialKit;
    public GameObject tutorialStatus;
    public GameObject tutorialMap;
    public GameObject tutorialGoal;
    public GameObject exitScene;
    public GameObject gameOver;

    public int n_tutorialCount = 0;

    bool b_tutorialCheck = true;

    static GameManager m_cInstance;

    static public GameManager GetInstance()
    {
        return m_cInstance;
    }
    // Use this for initialization
    void Start () {
        m_cInstance = this;
        //CreateBullet();
	}
	
	// Update is called once per frame
	void Update () {
        TutorialStep();
        ExitCheck();
        EndGame();

    }

    void EndGame()
    {
        if (c_player.b_deathCheck)
        {
            Time.timeScale = 0;
            gameOver.SetActive(true);
        }
    }

    void TutorialStep()
    {
        if (n_tutorialCount >= 0)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            n_tutorialCount = 0;
            b_tutorialCheck = true;
        }
        if (b_tutorialCheck)
        {
            if (n_tutorialCount == 0)
            {
                tutorialMove.SetActive(true);
                tutorialFire.SetActive(false);
            }

            if (n_tutorialCount == 1)
            {
                tutorialMove.SetActive(false);
                tutorialFire.SetActive(true);
                tutorialUiWindow.SetActive(false);
            }

            if (n_tutorialCount == 2)
            {
                tutorialFire.SetActive(false);
                tutorialUiWindow.SetActive(true);
                tutorialBullet.SetActive(false);
            }

            if (n_tutorialCount == 3)
            {
                tutorialUiWindow.SetActive(false);
                tutorialBullet.SetActive(true);
                tutorialKit.SetActive(false);
            }

            if (n_tutorialCount == 4)
            {
                tutorialBullet.SetActive(false);
                tutorialKit.SetActive(true);
                tutorialStatus.SetActive(false);
            }

            if (n_tutorialCount == 5)
            {
                tutorialKit.SetActive(false);
                tutorialStatus.SetActive(true);
                tutorialMap.SetActive(false);
            }

            if (n_tutorialCount == 6)
            {
                tutorialStatus.SetActive(false);
                tutorialMap.SetActive(true);
                tutorialGoal.SetActive(false);
            }

            if (n_tutorialCount == 7)
            {
                tutorialMap.SetActive(false);
                tutorialGoal.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                n_tutorialCount = -1;
                tutorialMove.SetActive(false);
                tutorialFire.SetActive(false);
                tutorialUiWindow.SetActive(false);
                tutorialBullet.SetActive(false);
                tutorialKit.SetActive(false);
                tutorialStatus.SetActive(false);
                tutorialMap.SetActive(false);
                tutorialGoal.SetActive(false);
                b_tutorialCheck = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            n_tutorialCount++;
            if (n_tutorialCount >= 7)
            {
                n_tutorialCount = 7;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            n_tutorialCount--;
            if (n_tutorialCount <= 0)
            {
                n_tutorialCount = 0;
            }
        }
    }

    void ExitCheck()
    {
        if (c_exit.b_exitCheck == true)
        {
            Time.timeScale = 0;
            exitScene.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1;
            exitScene.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

        }
    }

    public void CreateBullet()
    {
        c_bulletManager.m_bulletList.Add(new BulletType("일반탄", 10, 0.5f, 100));
        c_bulletManager.m_bulletList.Add(new BulletType("파워탄", 15, 0.5f, 100));
        c_bulletManager.m_bulletList.Add(new BulletType("특수탄", 20, 0.5f, 100));
    }
}
