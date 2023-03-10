using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer: MonoBehaviour{

    Action executeMethod;
    float executeTime;
    public float timer;
    public bool loop = true;

    bool running = false;

    public void Construct(Action eM, float eT,bool loops)
    {
        executeMethod = eM;
        executeTime = eT;
        loop = loops;
    }

    public void Run()
    {
        running = true;
    }

    void Update () {
        if (running)
        {
            timer += Time.deltaTime;
            if (timer >= executeTime)
            {
                executeMethod();
                timer = 0;
                if (!loop)
                {
                    running = false;
                }
            }
        }
	}

    public void UpdateEndTime(float eT)
    {
        executeTime = eT;
    }
}
