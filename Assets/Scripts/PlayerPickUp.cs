using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour {

    #region pub vars
    public string type = "goo";
    public float addVal = 10;
    public AudioClip pickUpSound;
    public float lifeTime = 5;
    #endregion

    #region priv vars
    AudioSource au;
    Timer lifeTimer;
    SpriteRenderer sr;
    #endregion

    void Start()
    {
        au = GameObject.Find("MiscSoundKeeper").GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        lifeTimer = gameObject.AddComponent(typeof(Timer)) as Timer;
        lifeTimer.Construct(End, lifeTime, false);
        lifeTimer.Run();
    }

    void Update()
    {
        Fade();
    }

    void PickUp(GameObject player)
    {
        float rand = Random.Range(0.5f, 1.5f);
        au.pitch = rand;
        au.PlayOneShot(pickUpSound,1);
        au.pitch = 1;
        switch(type)
        {
            case "goo":
                player.GetComponent<PlayerBrain>().gun.ModGoo(addVal);
                break;
            case "hp":
                player.GetComponent<Vitality>().ModHealth(addVal);
                break;
            case "sp":
                player.GetComponent<Combat>().ModStam(addVal);
                break;
        }
        Destroy(gameObject);
    }
    void End()
    {
        Destroy(gameObject);
    }
    void Fade()
    {
        sr.color = new Color (1,1,1,((lifeTime - lifeTimer.timer) / lifeTime) + .1f);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player")
        {
            PickUp(other.transform.parent.gameObject);
        }
    }
}
