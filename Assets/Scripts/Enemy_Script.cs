using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Script : MonoBehaviour {

    public GameObject player;

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
	}
	
	// Update is called once per frame
	void Update () {
        if (alive && transform.position.x > player.transform.position.x) transform.position = new Vector3(transform.position.x - (moveSpeed * Time.deltaTime), transform.position.y, transform.position.z);
	}

    public void Die()
    {
        Destroy(this.gameObject);
    }
}
