using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour {

    #region pub vars
    public float dieDistance;
    #endregion

    #region priv vars
    Transform player;
    [HideInInspector]
    public Action Die;
    #endregion

    void Start () {
        player = GameObject.Find("Player").transform;
	}
	
	void Update () {
        DieCheck();
	}

    void DieCheck()
    {
        if((player.transform.position-transform.position).magnitude<=dieDistance)
        {
            Die();
        }
    }
}
