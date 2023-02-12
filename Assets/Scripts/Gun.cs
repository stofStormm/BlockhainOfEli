using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    #region pub vars
    public GameObject bullet;
    public float shootRate = 1;
    [Header("Goo")]
    public float maxGoo = 10;
    public float gooCost = 1;
    public Bar gooBar;
    public AudioClip shootSound;
    #endregion

    #region priv vars
    Timer shootTimer;
    bool canShoot = true;
    float curGoo;
    [HideInInspector]
    public GlobalScript gs;
    AudioSource au;
    #endregion

    void Start () {
        au = GetComponent<AudioSource>();
        shootTimer = gameObject.AddComponent(typeof(Timer)) as Timer;
        shootTimer.Construct(CanShoot, shootRate, false);
        curGoo = maxGoo;
        if (gooBar != null)
        {
            gooBar.StartBar(maxGoo);
        }
        canShoot = true;
	}
	
    public void Shoot(Transform target)
    {
        if(curGoo-gooCost>=0 && gs.state==1 && canShoot)
        {
            float rand = Random.Range(0.5f, 1.5f);
            au.pitch = rand;
            au.PlayOneShot(shootSound,1);
            GameObject instBullet = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
            instBullet.GetComponent<Bullet1>().StartBullet(target);
            ModGoo(-gooCost);
            canShoot = false;
            shootTimer.Run();
        }
    }

    void CanShoot()
    {
        canShoot = true;
    }

    public void ModGoo(float val)
    {
        if (curGoo + val > maxGoo)
        {
            curGoo = maxGoo;
        }
        else if (curGoo + val < 0)
        {
            curGoo = 0;
        }
        else
        {
            curGoo += val;
        }
        if (gooBar != null)
        {
            gooBar.ModBar(curGoo);
        }
    }
}
