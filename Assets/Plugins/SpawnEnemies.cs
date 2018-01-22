using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class EnemiesClass
{
    public string NameEnemy;
    public GameObject EnemyPrefab;
    public int MaxHealth = 150;
    public int Rewarded = 10;
    public List<int> EnemyWeapons = new List<int>();
}

public class SpawnEnemies : MonoBehaviour
{
    public MainInput MInput;
    public Player PlayerScript;
    public List<EnemiesClass> Enemies = new List<EnemiesClass>();
    public Transform SpawnPoint;
    public Transform StartAttackPoint;

    public List<Transform> SpawnedBots = new List<Transform>();

	public void SpawnBot()
    {
        int rand = Random.Range(0, Enemies.Count);
        GameObject enemy = Instantiate(Enemies[rand].EnemyPrefab, SpawnPoint.position, SpawnPoint.rotation) as GameObject;
        //enemy = transform.GetChild(transform.childCount - 1);
        Bot bot = enemy.GetComponent<Bot>();
        bot.target_pos = StartAttackPoint.position;
        bot.doing = "run";
        transform.gameObject.layer = 2;
        Man tMan = enemy.GetComponent<Man>();
        tMan.health = Enemies[rand].MaxHealth;
        tMan.Rewarded = Enemies[rand].Rewarded;
        tMan.runing = true;
        tMan.speed_max = 2;
        enemy.GetComponent<Band>().weapon = Enemies[rand].EnemyWeapons.ToArray();
        SpawnedBots.Add(enemy.transform);
        MInput.MainElements.CountEnemy += 1;
        MInput.MainElements.CountEnemyesText.text = "Enemyes: " + MInput.MainElements.CountEnemy;
    }

    void Update()
    {
        if(SpawnedBots.Count > 0)
        {
            for(int i=0; i< SpawnedBots.Count; i++)
            {
                if (SpawnedBots[i] != null && Vector3.Distance(PlayerScript.Vector(SpawnedBots[i].transform.position.x, 0, SpawnedBots[i].transform.position.z),
                    PlayerScript.Vector(StartAttackPoint.transform.position.x,0, StartAttackPoint.transform.position.z)) < 2)
                {
                    SpawnedBots[i].gameObject.layer = 0;
                    SpawnedBots[i].GetComponent<Man>().killer = PlayerScript.player;
                    SpawnedBots[i].GetComponent<Bot>().doing = "idle";
                    SpawnedBots[i].GetComponent<Bot>().have_point = false;
                    SpawnedBots[i].GetComponent<Band>().StartAttack = true;
                    SpawnedBots.Remove(SpawnedBots[i]);
                }
            }
        }
        else if(transform.gameObject.layer != 0)
        {
            transform.gameObject.layer = 0;
        }
    }
}
