using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ANIMATION
{
    IDLE,
    POWERIDLE,
    ULTRAIDLE,
    ENEMYIDLE,
    BASIC1,
    BASIC2,
    BASIC3,
    BASIC4,
    BASIC5,
    POWER1,
    POWER2,
    POWER3,
    ULTRA1,
    ULTRA2,
    ULTRA3,
    ULTRA4,
    ULTRA5,
    RECOVERY1,
    RECOVERY2,
    SCORE1,
    SCORE2,
    SCORE3,
}

public class Player_Script : MonoBehaviour {

    public GameObject gameScript;
    public GameObject enemyManager;

    public Sprite[] sprites;
    private float animTimer;
    private int lastBasic;
    private int lastPower;
    private int lastUltra;

    public float attackRange;

    public float offsetToEnemy;

    private float timeSinceLastPunch;
    public float timePhasePower;
    public float timePhaseUltra;

    public GameObject playerSprite;

    [HideInInspector]
    public GameObject nextEnemy;


	// Use this for initialization
	void Start () {
        lastBasic = (int)ANIMATION.BASIC1;
        lastPower = (int)ANIMATION.POWER1;
        lastUltra = (int)ANIMATION.ULTRA1;
        animTimer = 0;
    }
	
	// Update is called once per frame
	void Update () {

        if (enemyManager.GetComponent<EnemyManager_Script>().GetAlive() > 0) nextEnemy = enemyManager.GetComponent<EnemyManager_Script>().enemyList[enemyManager.GetComponent<EnemyManager_Script>().GetAlive() - 1];
        else if (enemyManager.GetComponent<EnemyManager_Script>().GetAlive() == 0) nextEnemy = null;

        if (Input.GetKeyDown(KeyCode.Space) && nextEnemy != null && nextEnemy.transform.position.x - transform.position.x <= attackRange && animTimer < 0)
        {
            Punch();

            timeSinceLastPunch = 0;
        }

        timeSinceLastPunch += Time.deltaTime;

        animTimer -= Time.deltaTime;
        if (animTimer < 0 && animTimer > -timePhasePower) playerSprite.GetComponent<SpriteRenderer>().sprite = sprites[(int)ANIMATION.IDLE];
        //else if (animTimer < -timePhasePower && animTimer > -(timePhaseUltra + timePhasePower)) playerSprite.GetComponent<SpriteRenderer>().sprite = sprites[(int)ANIMATION.POWERIDLE];
        //else if (animTimer < -(timePhaseUltra + timePhasePower)) playerSprite.GetComponent<SpriteRenderer>().sprite = sprites[(int)ANIMATION.ULTRAIDLE];

    }

    void Punch()
    {
        gameScript.GetComponent<Game_Script>().ShiftLevel(nextEnemy.transform.position.x - transform.position.x + offsetToEnemy);

        if (timeSinceLastPunch > timePhaseUltra)
        {
            HeavyPunch();
        }
        
        else if (timeSinceLastPunch > timePhasePower)
        {
            MediumPunch();
        }

        else LightPunch();
    }

    void LightPunch()
    {
        if (nextEnemy.GetComponent<Enemy_Script>().type == Enemy_Script.TYPE.NORMAL)
        {
            enemyManager.GetComponent<EnemyManager_Script>().Kill();

            int animToPlay = lastBasic;

            int nextdistance = Random.Range(1, (int)ANIMATION.BASIC4 - (int)ANIMATION.BASIC1);

            animToPlay += nextdistance;

            if (animToPlay > (int)ANIMATION.BASIC4) animToPlay = (int)ANIMATION.BASIC1 + animToPlay - (int)ANIMATION.BASIC4 - 1;

            lastBasic = animToPlay;

            PlayAnimation((ANIMATION)animToPlay, 0.5f);
        }
    }

    void MediumPunch()
    {
        if (nextEnemy.GetComponent<Enemy_Script>().type == Enemy_Script.TYPE.NORMAL ||
            nextEnemy.GetComponent<Enemy_Script>().type == Enemy_Script.TYPE.SUPER)
        {
            enemyManager.GetComponent<EnemyManager_Script>().Kill();

            int animToPlay = lastPower;

            int nextdistance = Random.Range(1, (int)ANIMATION.POWER2 - (int)ANIMATION.POWER1);

            animToPlay += nextdistance;

            if (animToPlay > (int)ANIMATION.BASIC4) animToPlay = (int)ANIMATION.POWER1 + animToPlay - (int)ANIMATION.POWER2 - 1;

            lastPower = animToPlay;

            PlayAnimation((ANIMATION)animToPlay, 0.5f);
        }
    }

    void HeavyPunch()
    {
        if (nextEnemy.GetComponent<Enemy_Script>().type == Enemy_Script.TYPE.NORMAL ||
            nextEnemy.GetComponent<Enemy_Script>().type == Enemy_Script.TYPE.SUPER ||
            nextEnemy.GetComponent<Enemy_Script>().type == Enemy_Script.TYPE.ULTRA)
        {
            enemyManager.GetComponent<EnemyManager_Script>().Kill();

            int animToPlay = lastUltra;

            int nextdistance = Random.Range(1, (int)ANIMATION.ULTRA2 - (int)ANIMATION.ULTRA1);

            animToPlay += nextdistance;

            if (animToPlay > (int)ANIMATION.BASIC4) animToPlay = (int)ANIMATION.ULTRA1 + animToPlay - (int)ANIMATION.ULTRA2 - 1;

            lastUltra = animToPlay;

            PlayAnimation((ANIMATION)animToPlay, 0.5f);
        }
    }

    void PlayAnimation(ANIMATION anim, float duration)
    {
        playerSprite.GetComponent<SpriteRenderer>().sprite = sprites[(int)anim];
        animTimer = duration;
    }
}
