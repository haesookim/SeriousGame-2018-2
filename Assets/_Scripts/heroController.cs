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

    private Rigidbody2D rb;
    private Animator anim;
    
    //check status of player if necessary
    //public Text status;

    //health and damage parameters
    public float health = 200;
    public float damage = 10;
    public float attackSpeed;

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

        rb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
    }

    // Use this for initialization
    void Start () {
        vel = idle;
        rb = gameObject.GetComponent<Rigidbody2D>();

        //prevents player from rotating on collision
        rb.freezeRotation = true;

        layermask = ~(LayerMask.GetMask("Player"));
    }
	
	// Update is called once per frame
	void FixedUpdate () {
 
        Move();
        jump();

        if (Input.GetKeyDown(KeyCode.LeftControl) && canAttack){
            StartCoroutine(BasicAttack());
        }
        Debug.DrawRay(transform.position + new Vector3(0,1,0), transform.right * attackRange, Color.red);
    }

    private int direction = 1;
    void Move(){
        if (Input.GetKey(KeyCode.RightArrow))
        {
            vel = walkforce;
            gameObject.GetComponent<Animator>().SetBool("isWalking", true);
            gameObject.GetComponent<SpriteRenderer>().flipX = false;

            direction = 1;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            vel = -walkforce;
            gameObject.GetComponent<Animator>().SetBool("isWalking", true);
            gameObject.GetComponent<SpriteRenderer>().flipX = true;

            direction = -1;
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("isWalking", false);
            vel = idle;
        }
        gameObject.transform.position = gameObject.transform.position + vel * Time.deltaTime * 3;
    }

    private bool canJump = true;
    private void jump()
    {
        canJump = onGround();
        if(Input.GetKeyDown(KeyCode.LeftAlt) && canJump)
        {
            rb.AddForce(jumpforce, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
        }
        if (onGround())
        {
            anim.SetBool("isJumping", false);
        }
    }

    private bool onGround()
    {
        float velocity_y = rb.velocity.y;
        if(velocity_y == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public float attackRange;
    private int layermask;
    private bool canAttack = true;
    private IEnumerator BasicAttack()
    {
        canAttack = false;
        //Attack Animation
        anim.SetBool("isPunching", true);

        //Send raycast to get objects in line
        RaycastHit2D hit;

        if (direction == 1)
        {
            hit = Physics2D.Raycast(transform.position + new Vector3(0, 1, 0), transform.right, attackRange, layermask);
        }
        else
        {
            hit = Physics2D.Raycast(transform.position + new Vector3(0, 1, 0), -transform.right, attackRange, layermask);
        }
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Enemy")
            {
                EnemyController enemy = hit.collider.GetComponent<EnemyController>();
                enemy.HP -= damage;
            }
        }
        float attackTimer = Time.fixedTime + attackSpeed;
        while(attackTimer > Time.fixedTime)
        {
            yield return null;
        }
        anim.SetBool("isPunching", false);
        canAttack = true;
        yield return null;
    }
}
