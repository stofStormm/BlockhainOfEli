using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour {

    #region pub vars

    #endregion

    #region priv vars
    Camera cam;
    Vector2 mousePoint;
    #endregion

    void Start () {
        Cursor.visible = false;
        cam = Camera.main;
        mousePoint = cam.ScreenToWorldPoint(Input.mousePosition);
    }
	
	void Update () {
        mousePoint = cam.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePoint;
	}
}
