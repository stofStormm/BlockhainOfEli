using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puddled : MonoBehaviour {

    public float puddleTime = 200;
    Loco_TopDown loco;
    Timer puddleTimer;
    bool puddled = false;

	void Start () {
        loco = GetComponent<Loco_TopDown>();
	}
    public void InPuddle()
    {
        if (!puddled)
        {
            puddleTimer = gameObject.AddComponent(typeof(Timer)) as Timer;
            puddleTimer.Construct(OutPuddle, puddleTime, false);
            loco.imobile = true;
            puddled = true;
        }
        puddleTimer.Run();
    }

    void OutPuddle()
    {
        
        loco.imobile = false;
        puddled = false;
    }
}
