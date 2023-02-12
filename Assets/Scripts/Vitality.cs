using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vitality : MonoBehaviour {

    #region pub vars
    [Header("Health")]
    public float maxHealth = 50;
    public Bar healthBar;
    public AudioClip hurtSound;
    public float hurtSoundWait = .2f;
    public GameObject hurtSounder;
    #endregion

    #region priv vars
    float curHealth;
    [HideInInspector]
    public GlobalScript gs;
    [HideInInspector]
    public Action death;
    [HideInInspector]
    public bool immune = false;
    AudioSource au;
    Timer hurtSoundTimer;
    bool canPlaySOund=true;
    #endregion

    void Start () {
        hurtSounder = GameObject.Find("HurtSounder");
        au = hurtSounder.GetComponent<AudioSource>();
        hurtSoundTimer = gameObject.AddComponent(typeof(Timer)) as Timer;
        hurtSoundTimer.Construct(CanPlaySound, hurtSoundWait, false);
        curHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.StartBar(maxHealth);
        }
	}

    void Update()
    {
        /*if (Input.GetKey(KeyCode.Space))
        {
            ModHealth(-5);
        }*/
        if(curHealth<=0)
        {
            death();
        }
    }
	
    public void ModHealth(float val)
    {
        if (!(immune && val < 0))
        {
            if (curHealth + val > maxHealth)
            {
                curHealth = maxHealth;
            }
            else if (curHealth + val < 0)
            {
                curHealth = 0;
            }
            else
            {
                curHealth += val;
            }
            if (healthBar != null)
            {
                healthBar.ModBar(curHealth);
            }
        }
        if(!immune && val<0 && canPlaySOund)
        {
            au.PlayOneShot(hurtSound,10);
            hurtSoundTimer.Run();
            canPlaySOund = false;
        }
    }

    void CanPlaySound()
    {
        canPlaySOund = true;
    }
}
