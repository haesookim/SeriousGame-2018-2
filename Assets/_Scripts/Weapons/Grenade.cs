using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour {

    //speed of the projectile - probably needs to be 
    public Vector3 shootforce = new Vector3(5f, 0);
    public float speed = 5;
    private Rigidbody2D rb;

    //takes 2 seconds before exploding
    public float delay = 2;

	// Use this for initialization
	void Start () {
        //rb = gameObject.GetComponent<Rigidbody2D>();
        //rb.AddForce(shootforce);
        //rb.velocity = this.transform.forward * 10;

        Invoke("Detonate", delay);
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(transform.forward * speed * Time.deltaTime);
	}

    void Detonate(){

        Destroy(gameObject);
    }
}
