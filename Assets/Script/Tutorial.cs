using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour {
    public GameObject tutorialMove;
    public GameObject tutorialFire;
    public GameObject tutorialUiWindow;
    public GameObject tutorialBullet;
    public GameObject tutorialKit;
    public GameObject tutorialStatus;
    public GameObject tutorialMap;
    public GameObject tutorialGoal;

    int n_tutorialCount = 0;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void TutorialStep()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            n_tutorialCount = 0;
        }

        if (n_tutorialCount == 0)
        {
            tutorialMove.SetActive(true);
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                n_tutorialCount++;
            }
        }

        if (n_tutorialCount == 1)
        {
            tutorialMove.SetActive(false);
            tutorialFire.SetActive(true);
            tutorialUiWindow.SetActive(false);
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                n_tutorialCount++;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                n_tutorialCount--;
            }
        }

        if (n_tutorialCount == 2)
        {
            tutorialFire.SetActive(false);
            tutorialUiWindow.SetActive(true);
            tutorialBullet.SetActive(false);
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                n_tutorialCount++;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                n_tutorialCount--;
            }
        }

        if (n_tutorialCount == 3)
        {
            tutorialUiWindow.SetActive(false);
            tutorialBullet.SetActive(true);
            tutorialKit.SetActive(false);
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                n_tutorialCount++;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                n_tutorialCount--;
            }
        }

        if (n_tutorialCount == 4)
        {
            tutorialBullet.SetActive(false);
            tutorialKit.SetActive(true);
            tutorialStatus.SetActive(false);
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                n_tutorialCount++;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                n_tutorialCount--;
            }
        }

        if (n_tutorialCount == 5)
        {
            tutorialKit.SetActive(false);
            tutorialStatus.SetActive(true);
            tutorialMap.SetActive(false);
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                n_tutorialCount++;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                n_tutorialCount--;
            }
        }

        if (n_tutorialCount == 6)
        {
            tutorialStatus.SetActive(false);
            tutorialMap.SetActive(true);
            tutorialGoal.SetActive(false);
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                n_tutorialCount++;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                n_tutorialCount--;
            }
        }

        if (n_tutorialCount == 7)
        {
            tutorialMap.SetActive(false);
            tutorialGoal.SetActive(true);
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                n_tutorialCount--;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                tutorialGoal.SetActive(false);
            }
        }
    }
}
