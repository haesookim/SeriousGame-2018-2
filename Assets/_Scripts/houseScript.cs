using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class houseScript : MonoBehaviour {

    public float health;
    public Sprite destroyed;

	// Use this for initialization
    void Start () {
        health = 100;
	}
	
	// Update is called once per frame
	void Update () {
        if (health < 0){
            gameObject.GetComponent<SpriteRenderer>().sprite = destroyed;
        }
	}
}
