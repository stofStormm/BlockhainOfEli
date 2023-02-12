using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2Puddle : MonoBehaviour {

    #region pub vars
    public float lifetime = 1;
    public GameObject puddle;
    #endregion

    #region priv vars
    Timer lifetimer;
    bool targetDie;
    Vector2 targetPos;
    #endregion

    public void StartB2P (bool lifeDie, Vector2 target) {
        if (lifeDie)
        {
            lifetimer = gameObject.AddComponent(typeof(Timer)) as Timer;
            lifetimer.Construct(Die, lifetime, false);
            lifetimer.Run();
        }
        else
        {
            targetDie = true;
            targetPos = target;
        }
	}

    void Update()
    {
        if(targetDie)
        {
            CheckTarget();
        }
    }

    public void CheckTarget()
    {
        
        if((targetPos-new Vector2(transform.position.x,transform.position.y)).magnitude<=.1f)
        {
            Die();
        }
    }

    public void Die()
    {
        Instantiate(puddle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
