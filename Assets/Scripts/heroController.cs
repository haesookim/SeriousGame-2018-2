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
    //public Text status;

    //health and damage parameters
    public float health = 200;
    public float damage = 10;

    //Don't Destroy on Load
    public static GameObject Player;
    private void Awake()
    {
        if(Player == null)
        {
            DontDestroyOnLoad(this.gameObject);
            Player = this.gameObject;
        }
        else
        {
            if(Player != this.gameObject)
            {
                Destroy(this.gameObject);
            }
        }
    }

    // Use this for initialization
    void Start () {
        vel = idle;
        rigid = gameObject.GetComponent<Rigidbody2D>();

        //prevents player from rotating on collision
        rigid.freezeRotation = true;

        layermask = ~(LayerMask.GetMask("Player"));
    }
	
	// Update is called once per frame
	void FixedUpdate () {
 
        Move();

        if (Input.GetKeyDown(KeyCode.LeftControl)){
            BasicAttack();
        }
        Debug.DrawRay(transform.position + new Vector3(0,1,0), transform.right * attackRange, Color.red);
    }

    private int direction = 1;
    void Move(){
        if (Input.GetKey(KeyCode.RightArrow))
        {
            vel = walkforce;
            gameObject.GetComponent<Animator>().SetBool("walking", true);
            gameObject.GetComponent<SpriteRenderer>().flipX = false;

            direction = 1;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            vel = -walkforce;
            gameObject.GetComponent<Animator>().SetBool("walking", true);
            gameObject.GetComponent<SpriteRenderer>().flipX = true;

            direction = -1;
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
                jump = false;
            }
        } else{
            gameObject.GetComponent<Animator>().SetBool("jump", false);
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

    public float attackRange;
    private int layermask;
    void BasicAttack()
    {
        //Attack Animation
        gameObject.GetComponent<Animator>().SetTrigger("attack");

        //Send raycast to get objects in line
        RaycastHit2D hit;

        if (direction == 1)
        {
            hit = Physics2D.Raycast(transform.position + new Vector3(0,1,0), transform.right, attackRange, layermask);
        }
        else
        {
            hit = Physics2D.Raycast(transform.position + new Vector3(0, 1, 0), -transform.right, attackRange, layermask);
        }
        if(hit.collider == null)
        {
            return;
        }
        if (hit.collider.tag == "Enemy")
        {
            EnemyController enemy = hit.collider.GetComponent<EnemyController>();
            enemy.HP -= damage;
        }
    }
}
