using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class heroController : MonoBehaviour {

    //movement variables
    private Vector3 vel;
    private Vector3 idle = new Vector3(0, 0);
    public Vector3 jumpforce = new Vector3(0, 10);
    public Vector3 walkforce = new Vector3(1f, 0);

    Rigidbody2D rigid;

    public bool jump = false;

    //check status of player if necessary
    public Text status;

    //health and damage parameters
    public float health = 200;
    public float damage = 10;

    //attacking parameters
    public Collider2D enemyCollider;
    //note: this probably needs to be in the enemy list?
    //also note: create mechanism for attack order in enemies - closest enemy first?
    public bool attack_able;

    // Use this for initialization
    void Start () {
        vel = idle;
        rigid = gameObject.GetComponent<Rigidbody2D>();

        //prevents player from rotating on collision
        rigid.freezeRotation = true;
    }
	
	// Update is called once per frame
	void Update () {
        Move();

        if (Input.GetKeyDown(KeyCode.LeftControl)){
            BasicAttack();
        }
	}

    void Move(){
        status.text = "Moving";
        if (Input.GetKey(KeyCode.RightArrow))
        {
            vel = walkforce;
            gameObject.GetComponent<Animator>().SetBool("walking", true);
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            vel = -walkforce;
            gameObject.GetComponent<Animator>().SetBool("walking", true);
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("walking", false);
            vel = idle;
        }
        if (Input.GetKeyDown(KeyCode.LeftAlt) && isGrounded())
        {
            gameObject.GetComponent<Animator>().SetBool("jump", true);
            jump = true;
            rigid.AddForce(jumpforce, ForceMode2D.Impulse);
        }

        if (jump){
            if (isGrounded()){
                gameObject.GetComponent<Animator>().SetBool("jump", false);
                jump = false;
            }
        }
        gameObject.transform.position = gameObject.transform.position + vel * Time.deltaTime * 3;
    }

    //Needs a better way to check grounded position - might use Raycast
    bool isGrounded(){
        if (vel.y.Equals(0)){
            return true;
        }
        return false;
    }

    void BasicAttack(){
        status.text = "Attacking";
        if (attack_able)
        {
            Debug.Log("Boom");
            enemyCollider.GetComponentInParent<enemyController>().health -= damage;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Contact");
        if (other.tag == "Enemy") {
            enemyCollider = other;
            attack_able = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy"){
            attack_able = false;
        }
    }


}
