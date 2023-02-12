using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puddle : MonoBehaviour {

    #region pub vars
    public float startScale = 1;
    public float dieScale = .3f;
    public float shrinkRate = 1;
    public float noShrinkTime = 1;
    public bool stuckPuddle = true;
    public bool burnPuddle = false;
    public float burnDamage = .01f;
    public bool playerPuddle;
    public Color puddleCol;
    #endregion

    #region priv vars
    float curScale;
    Timer noShrinkTimer;
    bool startShrink = false;
    #endregion

    void Start () {
        GetComponent<SpriteRenderer>().color = puddleCol;
        transform.localScale = new Vector2(startScale, startScale);
        curScale = transform.localScale.x;
        noShrinkTimer = gameObject.AddComponent(typeof(Timer)) as Timer;
        noShrinkTimer.Construct(StartShrink, noShrinkTime, false);
        noShrinkTimer.Run();
    }
	
	void Update () {
        if (startShrink)
        {
            Shrink();
        }
	}

    void Shrink()
    {
        curScale = transform.localScale.x - (shrinkRate * Time.deltaTime);
        transform.localScale = new Vector2(curScale, curScale);
        DieCheck();
    }

    void DieCheck()
    {
        if(transform.localScale.x<=dieScale)
        {
            Destroy(gameObject);
        }
    }

    public void StartShrink()
    {
        startShrink = true;
    }

    void OnTriggerEnter2D(Collider2D other)
{
    if(playerPuddle && stuckPuddle && other.tag== "EnemyFeet")
    {
            other.transform.parent.GetComponent<Loco_TopDown>().imobile = true;
            other.transform.parent.GetComponent<Loco_TopDown>().stuck = true;
       }
}
    void OnTriggerExit2D(Collider2D other)
    {
        if (playerPuddle && stuckPuddle && other.tag == "EnemyFeet")
        {
            other.transform.parent.GetComponent<Loco_TopDown>().imobile = false;
            other.transform.parent.GetComponent<Loco_TopDown>().stuck = false;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(burnPuddle)
        {
            if((other.tag == "EnemyFeet" && playerPuddle)|| (other.tag == "PlayerFeet" && !playerPuddle))
            {
                other.transform.parent.GetComponent<Vitality>().ModHealth(-burnDamage);
            }
        }
    }
}
