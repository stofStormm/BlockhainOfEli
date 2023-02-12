using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour {

    #region pub vars
    public float damage=1;
    public bool playerWeapon=false;
    public bool shakes = false;
    public CameraScript cam;
    #endregion

    #region priv vars

    #endregion
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Enemy" & playerWeapon)
        {
            other.transform.parent.gameObject.GetComponent<Vitality>().ModHealth(-damage);
            if(shakes)
            {
                cam.shake = true;
            }
        }
        if (other.tag == "Player" & !playerWeapon)
        {
            other.transform.parent.gameObject.GetComponent<Vitality>().ModHealth(-damage);
            other.transform.parent.gameObject.GetComponent<Combat>().Stagger(new Vector2(transform.position.x,transform.position.y));
        }
    }
}
