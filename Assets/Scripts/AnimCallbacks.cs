using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimCallbacks : MonoBehaviour {

    #region pub vars
    public Loco_TopDown loco;
    #endregion

    #region priv vars
    GlobalScript gs;
    #endregion

    void Start()
    {
        gs = GameObject.Find("GlobalController").GetComponent < GlobalScript>();
    }

    public void CanMove()
    {
        if (!loco.stuck)
        {
            loco.imobile = false;
        }
    }
    public void CantMove()
    {
        loco.imobile = true;
    }
    public void SelfDestruct()
    {
        Destroy(gameObject);
    }

    public void DeactivateSelf()
    {
        gameObject.SetActive(false);
    }

    public void Menu()
    {
        gs.Menu();
    }
}
