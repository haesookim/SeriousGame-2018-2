using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour {
 
    public Vector3 shootforce;
    private Rigidbody2D rb;
    private bool shot = false;
    public int direction;
    public float damage = 70;

    public GameObject explosionEffect;

    //takes 2 seconds before exploding
    public float delay = 2;

	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
        Invoke("Detonate", delay);
    }
	
	// Update is called once per frame
	void Update () {
        if (!shot){
            shootforce.x = shootforce.x * direction;
            rb.AddForce(shootforce);
            shot = true;
        }
    }

    void Detonate(){
        var exp = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(exp, 1);
    }
}
