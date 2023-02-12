using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBaby : MonoBehaviour {

    public AudioClip sound;
    AudioSource au;
	void Start () {
        au = GameObject.Find("MiscSoundKeeper").GetComponent<AudioSource>();
        float rand = Random.Range(0.5f, 1.5f);
        au.pitch = rand;
        au.PlayOneShot(sound,1);
        au.pitch = 1;
    }
}
