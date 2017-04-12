using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ENEMYANIMATION
{
    IDLE,
    BASIC1,
    WALK1,
    WALK2,
    WALK3,
    WALK4,
    WALK5,
    HIT1,
    HIT2,
    HIT3,
    POWIDLE,
    POWBASIC1,
    POWWALK1,
    POWWALK2,
    POWWALK3,
    POWWALK4,
    POWWALK5,
    POWHIT1,
    POWHIT2,
    POWHIT3,
    ULTIDLE,
    ULTBASIC1,
    ULTWALK1,
    ULTWALK2,
    ULTWALK3,
    ULTWALK4,
    ULTWALK5,
    ULTHIT1,
    ULTHIT2,
    ULTHIT3,
}

public class EnemyManager_Script : MonoBehaviour {

    public GameObject enemyPrefab;
    public GameObject player;

    public float offset;

    private float   spawnTimer;
    private int     groupSpawning;
    private int     groupCounter;
    private int     groupSpawned;
    private float   totalGroupTime;

    public Sequence[] groups;

    public Sprite[] sprites;

    [HideInInspector]
    public Queue<GameObject> enemyList;

    // Use this for initialization
    void Start()
    {
        enemyList = new Queue<GameObject>();
        spawnTimer = 0;
        totalGroupTime = 0;
        groupSpawning = -1;
        groupCounter = -1;
        groupSpawned = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = new Vector3(player.transform.position.x + offset, transform.position.y, transform.position.z);

        if (groupSpawning >= 0 && groupCounter > groups[groupSpawning].type.Length)
        {
            spawnTimer = 0;
            groupSpawning = -1;
            groupCounter = 0;
            totalGroupTime = 0;
            groupSpawned = 0;
        }

        if (spawnTimer < 0)
        {
            spawnTimer = 0;
            groupSpawning = -1;
            groupCounter = 0;
            totalGroupTime = 0;
            groupSpawned = 0;
        }

        if (groupSpawning > -1)
        {
            if (groupCounter == groupSpawned)
            {
                Spawn(groups[groupSpawning].type[groupCounter]);
                groupSpawned++;
            }

            float totalTime = 0;

            for (int i = 0; i < groupCounter + 1; i++)
            {
                totalTime += groups[groupSpawning].time[i];
            }

            if (spawnTimer <= totalGroupTime - totalTime) groupCounter++;
        }
        print(spawnTimer);
        spawnTimer -= Time.deltaTime;
	}

    public void Spawn(Enemy_Script.TYPE t)
    {
        GameObject tempEnemy = Instantiate(enemyPrefab);

        Enemy_Script script = tempEnemy.GetComponent<Enemy_Script>();

        script.player = player;
        script.alive = true;
        script.type = t;
        script.manager = this.gameObject;

        tempEnemy.transform.position = this.transform.position;

        enemyList.Enqueue(tempEnemy);
    }

    public void Kill()
    {
        GameObject temp = enemyList.Dequeue();
        Destroy(temp);
    }

    public void SpawnGroup(int index)
    {
        groupSpawning = index;
        spawnTimer = 0;
        groupCounter = 0;
        totalGroupTime = 0;
        groupSpawned = 0;
        for (int i = 0; i < groups[index].GetComponent<Sequence>().type.Length; i++)
        {
            spawnTimer += groups[index].GetComponent<Sequence>().time[i];
        }
        totalGroupTime = spawnTimer;
    }
}
