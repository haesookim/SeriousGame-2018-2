using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    public float movementSpeed;
    public float jumpForce;

    private KeyCode moveRight = KeyCode.RightArrow;
    private KeyCode moveLeft  = KeyCode.LeftArrow;
    private KeyCode jump      = KeyCode.Space;

    private Animator anim;
    private bool isRunning;

	// Use this for initialization
	void Start () {
        anim = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        playerMovement();
        anim.SetBool("isRunning", isRunning);
	}

    void playerMovement()
    {
        if (Input.GetKey(moveRight))
        {
            transform.Translate(movementSpeed * Time.deltaTime, 0, 0);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            isRunning = true;
        }
        else if (Input.GetKey(moveLeft))
        {
            transform.Translate(movementSpeed * Time.deltaTime, 0, 0);
            transform.rotation = Quaternion.Euler(0, 180, 0);
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
        if (Input.GetKeyDown(jump))
        {
            this.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce);
        }
    }

}
