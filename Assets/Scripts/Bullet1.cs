using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet1 : MonoBehaviour {

    #region pub vars
    public bool playerBullet = true;
    public float speed=1;
    public float damage=1;
    #endregion

    #region priv vars
    Rigidbody2D rb;
    Vector2 dir;
    Bullet2Puddle b2p;
    bool init = false;
    Vector2 targetPos;
    #endregion

    public void StartBullet (Transform target) {
        targetPos = new Vector2(target.position.x, target.position.y);
        dir = (target.position - transform.position).normalized;
        rb = GetComponent<Rigidbody2D>();
        b2p = GetComponent<Bullet2Puddle>();
        if (b2p != null)
        {
            b2p.StartB2P(false, targetPos);
        }

        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg;

        if (angle >100 && angle<270)
        {
        angle += 180;
        }
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = q;
        init = true;
    }

    void Update () {
        if (init)
        {
            Move();
        }
    }

    void Move()
    {
        rb.velocity = speed * dir;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="RoomWall")
        {
            if (b2p != null)
            {
                b2p.Die();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        if((playerBullet && other.tag=="Enemy") || (!playerBullet && other.tag == "Player"))
        {
            if (b2p != null)
            {
                b2p.Die();
            }
            else
            {
                other.transform.parent.gameObject.GetComponent<Vitality>().ModHealth(-damage);
                other.transform.parent.gameObject.GetComponent<Combat>().Stagger(new Vector2(transform.position.x, transform.position.y));
                Destroy(gameObject);
            }
        }
    }
}
