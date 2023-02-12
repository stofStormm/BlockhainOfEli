using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawn : MonoBehaviour {

    #region pub vars
    [Header("Spawn Locations")]
    public Transform top;
    public Transform right;
    public Transform bottom;
    public Transform left;
    public bool horizontal = true;
    [Header("Enemies")]
    public GameObject[] enemySet1;
    public int[] enemy1MinCount;
    public int[] enemy1MaxCount;
    public GameObject[] enemySet2;
    public int[] enemy2MinCount;
    public int[] enemy2MaxCount;
    public GameObject[] enemySet3;
    public int[] enemy3MinCount;
    public int[] enemy3MaxCount;
    public GameObject[] enemySet4;
    public int[] enemy4MinCount;
    public int[] enemy4MaxCount;
    public GameObject[] enemySet5;
    public int[] enemy5MinCount;
    public int[] enemy5MaxCount;
    public GameObject[] enemySet6;
    public int[] enemy6MinCount;
    public int[] enemy6MaxCount;
    public GameObject[] enemySet7;
    public int[] enemy7MinCount;
    public int[] enemy7MaxCount;
    public GameObject[] enemySet8;
    public int[] enemy8MinCount;
    public int[] enemy8MaxCount;
    public GameObject[] enemySet9;
    public int[] enemy9MinCount;
    public int[] enemy9MaxCount;
    public GameObject[] enemySet10;
    public int[] enemy10MinCount;
    public int[] enemy10MaxCount;
    [Header("Spawn Settings")]
    public int waveCount = 1;
    public float spawnRate = 1;
    public int missFireChance = 75;
    public GameObject waveText;
    public SpawnBrain sb;
    #endregion

    #region priv vars
    Timer spawnTimer;
    float[] bounds;
    [HideInInspector]
    public int curWave;
    GameObject[][] fullWaves;
    int[][] minEn;
    int[][] maxEn;
    int[] count;
    int counter;
    [HideInInspector]
    public bool spawning = false;
    [HideInInspector]
    public List<GameObject> spawnedEnemies;
    #endregion

    public void StartSpawner (int wave) {
        spawnedEnemies = new List<GameObject>();
        curWave = wave;
        fullWaves = new GameObject[][] { enemySet1,enemySet2,enemySet3, enemySet4, enemySet5, enemySet6, enemySet7, enemySet8, enemySet9, enemySet10};
        minEn = new int[][] { enemy1MinCount, enemy2MinCount , enemy3MinCount, enemy4MinCount, enemy5MinCount, enemy6MinCount, enemy7MinCount, enemy8MinCount, enemy9MinCount, enemy10MinCount};
        maxEn = new int[][] { enemy1MaxCount, enemy2MaxCount, enemy3MaxCount, enemy5MaxCount, enemy5MaxCount , enemy6MaxCount , enemy7MaxCount , enemy8MaxCount , enemy9MaxCount , enemy10MaxCount };
        SetBounds();
        StartSet();
        spawnTimer = gameObject.AddComponent(typeof(Timer)) as Timer;
        spawnTimer.Construct(SpawnEnemy, spawnRate, true);
        spawnTimer.Run();
	}

    void SpawnEnemy()
    {
        if (spawning)
        {
            int missCheck = Random.Range(0, 100);
            if (missCheck > missFireChance)
            {
                int rand = 0;
                while (count[rand] - 1 < 0)
                {
                    rand = Random.Range(0, fullWaves[curWave].Length);
                }
                count[rand] -= 1;
                Vector2 pos = Vector2.zero;
                if (horizontal)
                {
                    pos = new Vector2(Random.Range(bounds[0], bounds[1]), transform.position.y);
                }
                else
                {
                    pos = new Vector2(transform.position.x, Random.Range(bounds[0], bounds[1]));
                }

                GameObject instEnemy=Instantiate(fullWaves[curWave][rand], pos, Quaternion.identity)as GameObject;
                spawnedEnemies.Add(instEnemy);
                counter--;
            }
            if (counter == 0)
            {
                EndSet();
            }
        }
    }

    void SetBounds()
    {
        if(horizontal)
        {
            bounds =new float[] { left.position.x,right.position.x};
        }
        else
        {
            bounds = new float[] { bottom.position.y, top.position.y };
        }
    }

    public void StartSet()
    {
        
        counter = 0;
        count = new int[fullWaves[curWave].Length];
        for(int i =0;i<count.Length;i++)
        {
            count[i] = Random.Range(minEn[curWave][i], maxEn[curWave][i]);
            counter += count[i] ;
        }
        if(waveText!=null)
        {
            waveText.GetComponent<Text>().text = "Wave " + (curWave+1).ToString();
            waveText.SetActive(true);
        }
        spawnedEnemies = new List<GameObject>();
        spawning = true;
    }

    void EndSet()
    {
        spawning = false;
        curWave++;
    }
}
