using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Script : MonoBehaviour {

    public GameObject player;
    public GameObject enemyManager;
    public GameObject mainCamera;

    private float shiftTimer;

    public float cameraOffset;

    public float cameraDelay;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            enemyManager.GetComponent<EnemyManager_Script>().SpawnGroup(0);
        }

        shiftTimer -= Time.deltaTime;
        if (shiftTimer < 0)
        {
            mainCamera.transform.position = new Vector3(player.transform.position.x - cameraOffset, mainCamera.transform.position.y, mainCamera.transform.position.z);
        }

        if (player.GetComponent<Player_Script>().hp <= 0)
        {
            Lose();
        }
	}

    public void ShiftCamera ()
    {
        shiftTimer = cameraDelay;
    }

    public void Lose()
    {

    }
}
