using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBrain : MonoBehaviour {

    #region pub vars
    public EnemySpawn[] spawners;
    public GameObject winText;
    #endregion

    #region priv vars
    public int waveCount;
    public int curWave = 0;
    bool spawning;
    int enemyCount = 0;
    #endregion

    void Start () {
        InitSpawners();
	}
	

	void Update () {
        spawning = CheckSpawning();
        if (!spawning)
        {
            CheckEnemyCount();
        }
	}

    bool CheckSpawning()
    {
        bool test = false;
        for(int i =0;i<spawners.Length;i++)
        {
            if(spawners[i].spawning)
            {
                test = true;
            }
        }
        return test;
    }

    void InitSpawners()
    {
        for (int i = 0; i < spawners.Length; i++)
        {
            spawners[i].StartSpawner(curWave);
            spawners[i].waveCount=waveCount;
        }
    }

    void StartWave()
    {
        enemyCount = 0;
        for (int i = 0; i < spawners.Length; i++)
        {
            spawners[i].StartSet();
        }
        curWave++;
    }

    void CheckEnemyCount()
    {
        enemyCount = 0;
        for (int i = 0; i < spawners.Length; i++)
        {
            for (int j = 0; j < spawners[i].spawnedEnemies.Count; j++)
            {
               if(spawners[i].spawnedEnemies[j]!=null)
                {
                    enemyCount++;
                }
            }
        }
        if(enemyCount==0 && curWave<waveCount)
        {
            StartWave();
        }
        else if(enemyCount == 0 && curWave ==waveCount)
        {
            winText.SetActive(true);
        }
    }
}
