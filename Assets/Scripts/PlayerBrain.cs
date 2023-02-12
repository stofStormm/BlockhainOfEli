using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBrain : MonoBehaviour {

    #region pub vars
    public Animator anim;
    public Gun gun;
    public Transform crosshair;
    #endregion

    #region priv vars
    Loco_TopDown loco;
    Combat melee;
    Vitality vitals;
    GlobalScript gs;
    #endregion

    void Awake () {
        Dep();
	}

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            melee.AttackMelee();
        }
        if (Input.GetMouseButton(1))
        {
            gun.Shoot(crosshair);
        }
    }
	
	void FixedUpdate () {
        loco.Move(Input.GetAxisRaw("Horizontal"), Input.GetAxis("Vertical"));
	}

    void Die()
    {
        gs.GameOver();
    }

    void Dep()
    {
        gs = GameObject.Find("GlobalController").GetComponent<GlobalScript>();
        loco = GetComponent<Loco_TopDown>();
        loco.anim = anim;
        melee = GetComponent<Combat>();
        melee.anim = anim;
        melee.gs = gs;
        vitals = GetComponent<Vitality>();
        vitals.gs = gs;
        vitals.death = Die;
        melee.vitals = vitals;
        melee.loco = loco;
        gun.gs = gs;
    }
}
