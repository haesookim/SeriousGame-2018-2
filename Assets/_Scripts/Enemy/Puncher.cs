using UnityEngine;
using System.Collections;

public class Puncher: MonoBehaviour
{
    //This script controls everything that has to do with enemy-specific functions
    //basically attack patterns, basic AI, damage etc
    public float damage = 10;

    public float movementSpeed;
    public float attackRange;
    public float noticeRange;
    public float attackSpeed;

    private Rigidbody2D rb;

    private Animator anim;

    GeneralManager GM;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        layermask = ~(LayerMask.GetMask("Enemy"));
    }

    private void Update()
    {
        chase_and_attack();
    }

    private void death()
    {
        /*if (GM.CurrentSubMission.name_to_kill == this.tag)
        {
            GM.CurrentSubMission.killAmount_progress++;
        }*/
        Destroy(this.gameObject);
    }

    private void chase_and_attack()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");

        float distance = Vector2.Distance(Player.transform.position, this.transform.position);
        Debug.Log(distance);
        if (distance > attackRange && distance < noticeRange)
        {
            gameObject.GetComponentInParent<enemyController>().inactive = false;
            walk_towards_player(Player);
        }
        else if (distance <= attackRange)
        {
            if (canAttack)
                StartCoroutine(attack());
        }
        else{
            gameObject.GetComponentInParent<enemyController>().inactive = true;
        }
    }

    private int direction = 1;
    private void walk_towards_player(GameObject Player)
    {
        Vector2 playerPos = Player.transform.position;
        Vector2 enemyPos = this.transform.position;
        anim.SetBool("walking", true);
        if (playerPos.x < enemyPos.x)
        {
            //move left
            this.transform.Translate(movementSpeed * Time.deltaTime, 0, 0);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            direction = -1;
        }
        else
        {
            //move right
            this.transform.Translate(movementSpeed * Time.deltaTime, 0, 0);
            transform.rotation = Quaternion.Euler(0, 180f, 0);
            direction = 1;
        }
    }

    private bool canAttack = true;
    private IEnumerator attack()
    {
        canAttack = false;
        anim.SetTrigger("attack");
        attack_punch();

        float attackTimer = Time.fixedTime + attackSpeed;
        while (attackTimer > Time.fixedTime)
        {
            anim.SetBool("walking", false);
            yield return null;
        }
        canAttack = true;
        yield return null;
    }

    private int layermask;
    private void attack_punch()
    {
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
        if (hit.collider == null)
        {
            return;
        }
        if (hit.collider.tag == "Player")
        {
            heroController hero = hit.collider.GetComponent<heroController>();
            hero.health -= damage;
        }
    }

}
