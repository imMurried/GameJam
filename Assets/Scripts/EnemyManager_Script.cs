using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager_Script : MonoBehaviour {

    public GameObject enemyPrefab;

    public int max_Enemies;
    private int array_Max;
    private int alive_count;

    [HideInInspector]
    public GameObject[] enemyList;

    // Use this for initialization
    void Start()
    {
        array_Max = max_Enemies;
        enemyList = new GameObject[array_Max];
        alive_count = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
	}

    public void Spawn(Enemy_Script.TYPE t)
    {
        if (alive_count >= array_Max) return;

        GameObject tempEnemy = Instantiate(enemyPrefab);

        Enemy_Script script = tempEnemy.GetComponent<Enemy_Script>();

        script.alive = true;
        script.type = t;

        tempEnemy.transform.position = this.transform.position;

        enemyList[alive_count] = tempEnemy;
        alive_count++;
    }

    public void Kill()
    {
        if (alive_count > 0)
        {
            enemyList[alive_count - 1].GetComponent<Enemy_Script>().Die();
            alive_count--;
        }
    }

    public int GetAlive()
    {
        return alive_count;
    }
}
