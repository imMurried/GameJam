using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Script : MonoBehaviour {

    public GameObject player;
    public GameObject manager;

    public float walkframeinterval;
    private float walkTimer = 0;
    private int spriteFrame;
    private int activeSprite;

    public float moveSpeed;
    public TYPE type;
    public enum TYPE
    {
        NORMAL,
        SUPER,
        ULTRA,
    }

    //[HideInInspector]
    public bool alive;

	// Use this for initialization
	void Start () {

        spriteFrame = (int)ENEMYANIMATION.WALK1;

	}
	
	// Update is called once per frame
	void Update () {
        if (alive && transform.position.x > player.transform.position.x) transform.position = new Vector3(transform.position.x - (moveSpeed * Time.deltaTime), transform.position.y, transform.position.z);

        walkTimer -= Time.deltaTime;

        if (alive)
        {
            activeSprite = spriteFrame;

            if (type == TYPE.SUPER)
            {
                activeSprite = spriteFrame + 10;
            }

            if (type == TYPE.ULTRA)
            {
                activeSprite = spriteFrame + 20;
            }

            GetComponent<SpriteRenderer>().sprite = manager.GetComponent<EnemyManager_Script>().sprites[activeSprite];

            if (walkTimer < 0)
            {
                walkTimer = walkframeinterval;

                spriteFrame++;
                if (spriteFrame > (int)ENEMYANIMATION.WALK4)
                {
                    spriteFrame = (int)ENEMYANIMATION.WALK1;
                }
            }
        }
	}

    public void Die()
    {
        Destroy(this.gameObject);
    }
}
