using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour {

    #region pub vars
    [Header("Melee")]
    public float meleeRate = 1;
    public float meleeCost = 1;
    public GameObject weaponSounder;
    [Header("Stamina")]
    public float maxStam = 10;
    public Bar stamBar;
    public float stamRechargePause = 1;
    public float stamRechargeRate = 1;
    [Header("Stagger")]
    public bool staggers = false;
    public float staggerForce = 500;
    public float staggerTime = .5f;
    public AudioClip attackSound;
    #endregion

    #region priv vars
    [HideInInspector]
    public Animator anim;
    Timer meleeTimer;
    bool canAttack=true;
    float curStam;
    bool stamRecharging = true;
    Timer stamRechargeTimer;
    [HideInInspector]
    public GlobalScript gs;
    [HideInInspector]
    public Loco_TopDown loco;
    [HideInInspector]
    public Vitality vitals;
    Timer staggerTimer;
    AudioSource au;
    #endregion

    void Start () {
        au = GetComponent<AudioSource>();
        meleeTimer = gameObject.AddComponent(typeof(Timer)) as Timer;
        meleeTimer.Construct(CanAttack, meleeRate, false);
        curStam = maxStam;
        stamRechargeTimer = gameObject.AddComponent(typeof(Timer)) as Timer;
        stamRechargeTimer.Construct(CanRecharge, stamRechargePause, false);
        StartStamBar();
        staggerTimer = gameObject.AddComponent(typeof(Timer)) as Timer;
        staggerTimer.Construct(Unstagger, staggerTime, false);
    }
	
	void Update () {
		if(stamRecharging)
        {
            ModStam(stamRechargeRate);
        }
	}

    public void AttackMelee()
    {
        if (canAttack && curStam - meleeCost >= 0 && gs.state == 1) 
        {
            anim.SetTrigger("Melee");
            if (attackSound != null)
            {
                weaponSounder.GetComponent<AudioSource>().PlayOneShot(attackSound,1);
            }
            canAttack = false;
            ModStam(-meleeCost);
            meleeTimer.Run();
        }
    }

    void CanAttack()
    {
        canAttack = true;
    }

    void CanRecharge()
    {
        stamRecharging = true;
    }

    public void ModStam(float val)
    {
        if(curStam+val>maxStam)
        {
            curStam = maxStam;
        }
        else if (curStam+val<0)
        {
            curStam = 0;
        }
        else
        {
            curStam += val;
        }
        if(val<0)
        {
            stamRecharging = false;
            stamRechargeTimer.Run();
        }
        if(stamBar!=null)
        {
            stamBar.ModBar(curStam);
        }
    }

    void StartStamBar()
    {
        if (stamBar != null)
        {
            stamBar.StartBar(maxStam);
        }
    }

    public void Stagger(Vector2 hitLocation)
    {
        if (staggers)
        {
            if (!vitals.immune)
            {
                loco.floating = true;
                Vector2 forceDir = (hitLocation - new Vector2(transform.position.x, transform.position.y)).normalized;
                loco.rb.AddForce(staggerForce * -forceDir);
                staggerTimer.Run();
            }
        }
    }

    void Unstagger()
    {
        loco.rb.velocity = Vector2.zero;
        loco.floating = false;
        vitals.immune = false;
    }

}
