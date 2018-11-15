using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float HP;
    public float damage;

    public float movementSpeed;
    public float attackRange;
    public float attackSpeed;

    private Animator anim;

    GeneralManager GM;

    private void Awake()
    {
        GM = GameObject.FindObjectOfType<GeneralManager>();
        anim = GetComponent<Animator>();
        layermask = ~(LayerMask.GetMask("Enemy"));
    }

    private void Update()
    {
        chase_and_attack();
        death();
    }

    private void death()
    {
        if (HP <= 0)
        {
            if (GM.CurrentSubMission.name_to_kill == this.tag)
            {
                GM.CurrentSubMission.killAmount_progress++;
            }
            Destroy(this.gameObject);
        }
    }

    private void chase_and_attack()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");

        float distance = Vector2.Distance(Player.transform.position, this.transform.position);
        if(distance > attackRange)
        {
            walk_towards_player(Player);
        }
        else
        {
            StartCoroutine(attack());
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
            this.transform.Translate(-movementSpeed * Time.deltaTime, 0, 0);
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            direction = -1;
        }
        else
        {
            this.transform.Translate(movementSpeed * Time.deltaTime, 0, 0);
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
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
