using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyController : MonoBehaviour {

    //probably better to create a basic interface then import to all enemy types
    //would need: health, damage, etc...

    public float HP = 50;
    public float damage = 5;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (HP <= 0){
            Destroy(gameObject);
        }
	}
}
