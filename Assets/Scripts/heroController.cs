using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class heroController : MonoBehaviour {

    public Vector3 vel;

    // Use this for initialization
    void Start () {
        vel = new Vector3(0, 0);
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    void Move(){
        if (Input.GetKey(KeyCode.RightArrow))
        {
            vel = new Vector3(1, 0);
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            vel = new Vector3(-1, 0);
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            vel = new Vector3(0, 0);
        }
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            vel += new Vector3(0, 1);
        }
        gameObject.transform.position = gameObject.transform.position + vel * Time.deltaTime * 3;
    }

}
