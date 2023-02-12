using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Brain : MonoBehaviour {

    #region pub vars
    public Animator anim;
    public GameObject dieAnim;
    public float attackDist = 1;
    public bool shoots =false;
    public float shootRange = 5;
    public Gun gun;
    #endregion

    #region priv vars
    GlobalScript gs;
    GameObject player;
    Loco_TopDown loco;
    Vitality vitals;
    Vector2 toPlayer;
    Combat melee;
    SelfDestruct sd;
    #endregion

    void Awake () {
        Dep();
	}

    void Update()
    {
        ChasePlayer();
        CheckAttack();
        if(shoots && !loco.stuck)
        {
            ShootCheck();
        }
    }
	
	void FixedUpdate () {
        
	}

    void ChasePlayer()
    {
        toPlayer = (player.transform.position - transform.position);
        Vector2 dir =toPlayer.normalized;
        loco.Move(dir.x, dir.y);
    }

    void Die()
    {
        Instantiate(dieAnim, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void CheckAttack()
    {if (melee != null)
        {
            if (toPlayer.magnitude <= attackDist && ((loco.facingRight && toPlayer.x > 0) || (!loco.facingRight && toPlayer.x < 0)))
            {
                melee.AttackMelee();
            }
        }
    }

    void Dep()
    {
        gs = GameObject.Find("GlobalController").GetComponent<GlobalScript>();
        player = GameObject.Find("Player");
        loco = GetComponent<Loco_TopDown>();
        loco.anim = anim;
        vitals = GetComponent<Vitality>();
        vitals.death = Die;
        melee = GetComponent<Combat>();
        if (melee != null)
        {
            melee.anim = anim;
            melee.gs = gs;
        }
        if(shoots)
        {
            gun.gs = gs;
        }
        sd = GetComponent<SelfDestruct>();
        if(sd!=null)
        {
            sd.Die = Die;
        }
    }

    void ShootCheck()
    {
        if((player.transform.position-transform.position).magnitude<shootRange)
        {
            gun.Shoot(player.transform);
        }
    }

    /*void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag== "pu2")
        {
            GetComponent<Puddled>().InPuddle();
        }
    }*/
}
