using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public AudioClip afireSound1;
    public AudioClip areloadSound;
    AudioSource myAudio;

    public static SoundManager instance;

    void Awake()
    {
        if (SoundManager.instance == null)
        {
            SoundManager.instance = this;
        }
    }

    void Start () {
        myAudio = GetComponent<AudioSource>();
	}

    public void FireSound()
    {
        myAudio.PlayOneShot(afireSound1);
    }	

    public void ReloadSound()
    {
        myAudio.PlayOneShot(areloadSound);
    }

	void Update () {
		
	}
}
