using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour {

    #region pub vars
    public bool hideIfFull = false;
    #endregion

    #region priv vars
    [HideInInspector]
    float ratio = 0;
    SpriteRenderer sr;
    float fullValue;
    float width;
    #endregion

    public void StartBar (float fullVal) {
        fullValue = fullVal;
        sr = GetComponent<SpriteRenderer>();
        ratio = sr.size.x / fullVal;
        if(hideIfFull)
        {
            sr.color = new Color(1, 1, 1, 0);
        }
	}
	
    public void ModBar(float curValue)
    {
        width = ratio * curValue;
        sr.size = new Vector2(width, sr.size.y);
        if (hideIfFull && Mathf.Abs(fullValue-curValue)<0.1)
        {
            sr.color = new Color(1, 1, 1, 0);
        }
        else
            {

        }
        sr.color = new Color(1, 1, 1, 1);
    }
}
