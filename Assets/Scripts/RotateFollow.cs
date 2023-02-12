using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFollow : MonoBehaviour {

    #region pub var
    public Transform followObject;
    public bool permaFollow=true;
    public bool checkFlip = true;
    public GameObject flipLoco;
    public bool followPlayer=true;
    #endregion

    #region priv vars

    #endregion

    void Start () {
		if(followPlayer)
        {
            followObject = GameObject.Find("Player").transform;
        }
	}
	
	void Update () {
        if (permaFollow)
        {
            Follow(followObject.position);
        }
	}

    public void Follow(Vector2 targetPos)
    {
        float angle = Mathf.Atan2(targetPos.y- transform.position.y,  targetPos.x- transform.position.x ) * Mathf.Rad2Deg;
        if (checkFlip)
        {
            if (!flipLoco.GetComponent<Loco_TopDown>().facingRight)
            {
                angle += 180;
            }
        }
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = q;
    }
}
