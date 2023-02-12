using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loco_TopDown : MonoBehaviour {

    #region pub vars
    [Header("Movement")]
    public float runSpeed = 1;
    public bool flips = true;
    public bool facingRight = true;
    public AudioClip runSound;
    #endregion

    #region priv vars
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public bool imobile = false;
    [HideInInspector]
    public bool floating = false;
    float speed;
    [HideInInspector]
    public bool stuck = false;
    AudioSource au;
    #endregion
    void Start()
    {
        speed = runSpeed;
        rb = GetComponent<Rigidbody2D>();
        au = GetComponent<AudioSource>();
    }
	void Update () {
        CheckAnimation();
        if(flips)
        {
            FlipCheck();
        }
        if (imobile)
        {
            speed = 0;
        }
        else
        {
            speed = runSpeed;
        }
	}

    public void Move(float x, float y)
    {
        if (!floating)
        {
            float angle = 0;
            Vector2 vel = new Vector2(x, y);
            if (y != 0 || x != 0)
            {
                angle = Mathf.Abs(Mathf.Atan(y / x));
                vel = new Vector2(x * Mathf.Cos(angle), y * Mathf.Sin(angle)) * speed;
            }
            rb.velocity = vel;
        }
    }
    public void MoveSimplified(float x, float y)
    {
        if (!floating)
        {
            rb.velocity = new Vector2(x, y) * speed;
        }
    }
    public void CheckAnimation()
    {
        if(rb.velocity.magnitude!=0)
        {
            anim.SetBool("isRunning", true);
            if(runSound!=null)
            {
                if(!au.isPlaying)
                {
                    au.clip = runSound;
                    au.volume = 0.2f;
                    au.Play();
                }
            }
        }
        else
        {
            anim.SetBool("isRunning", false);
            if (runSound != null)
            {
                if (au.isPlaying && au.clip==runSound)
                {
                    au.volume = 1;
                    au.Stop();
                }
            }
        }
    }
    void FlipCheck()
    {
        if (!imobile)
        {
            if ((facingRight && rb.velocity.x < 0) || (!facingRight && rb.velocity.x > 0))
            {
                facingRight = !facingRight;
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            }
        }
    }
}
