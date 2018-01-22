using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MainSpawner : MonoBehaviour
{
    public MainInput MInput;
    public Player PlayerScript;
    public List<SpawnEnemies> SpawnPoints = new List<SpawnEnemies>();
    public int Level = 0;
    public int MaxEnemy = 6;

    void Start ()
    {
	    for(int i=0; i< SpawnPoints.Count; i++)
        {
            SpawnPoints[i].MInput = MInput;
            SpawnPoints[i].PlayerScript = PlayerScript;
        }
        Level = PlayerPrefs.GetInt("levelGame");
        StartCoroutine(SpawnEnemy(Level));
	}

    IEnumerator SpawnEnemy(int level)
    {
        yield return new WaitForSeconds(5);
        switch(level)
        {
            case 0:
                MaxEnemy = 6;
                break;
            case 1:
                MaxEnemy = 10;
                break;
            case 2:
                MaxEnemy = 14;
                break;
        }
        int rand = 0;
        while (true)
        {
            if (MInput.MainElements.CountEnemy < MaxEnemy)
            {
                int countMans = Random.Range(1, level == 0 ? 1 : level + 1);
                for (int i = 0; i < countMans; i++)
                {
                    rand = Random.Range(0, SpawnPoints.Count);
                    SpawnPoints[rand].SpawnBot();
                }
            }
            yield return new WaitForSeconds(20/(level==0?1:level+1));
        }
    }
}
