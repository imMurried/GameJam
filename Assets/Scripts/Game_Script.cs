using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Script : MonoBehaviour {

    public GameObject player;
    public GameObject enemyManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            enemyManager.GetComponent<EnemyManager_Script>().Spawn(Enemy_Script.TYPE.NORMAL);
        }
	}

    public void ShiftLevel (float amount)
    {

    }
}
