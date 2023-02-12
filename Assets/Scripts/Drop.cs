using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour {

    #region pub vars
    public GameObject[] drops;
    public int[] dropChance;
    public int[] doubleDropChance;
    public float deltaX = .5f;
    public float deltaY = .25f;
    #endregion

    #region priv vars
    #endregion

    void Start () {
	}

    public void DropCheck()
    {
        for(int i = 0;i<drops.Length;i++)
        {
            int rand = Random.Range(0, 100);
            if(rand>dropChance[i])
            {
                DropItem(drops[i]);
            }
            if (rand > doubleDropChance[i])
            {
                DropItem(drops[i]);
            }
        }
    }

    void DropItem(GameObject item)
    {
        Vector2 pos=new Vector2(transform.position.x+Random.Range(-deltaX, deltaX),transform.position.y+Random.Range(-deltaY, deltaY));
        Instantiate(item, pos, Quaternion.identity);
    }
	

}
