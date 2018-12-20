using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V2 : MonoBehaviour, Damageable {

    [Header("Stat")]
    [SerializeField] private float maximum_hp;
    [SerializeField] private float attack_damage;

    [Header("Attack")]
    [SerializeField] private float charge_speed;
    [SerializeField] private float charge_time;

    [Header("Walk")]
    [SerializeField] private float walk_time;
    [SerializeField] private float movement_speed;
    
    //------------------------------[Villain Components]
    private Animator anim;
    private Rigidbody2D rb;
    

    private float current_hp;

    private enum State {
        Walk,
        Charge,
        Dead
    }

    private State current_state = State.Walk;
    private bool can_damage = false;
    
    private bool can_start_pattern = true;

    void Start () {
        //Villain Components
        anim = this.GetComponentInChildren<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
        //Villain Stats
        current_hp = maximum_hp;
        gameObject.GetComponent<Villain_healthbar>().villain_Maximum_Hp = maximum_hp;
    }
	
	void Update () {
        villain_pattern();
        gameObject.GetComponent<Villain_healthbar>().villain_current_hp = current_hp;
    }

    private void villain_pattern() {
        if (can_start_pattern) {
            can_start_pattern = false;
            switch (current_state) {
                case State.Walk:
                    StartCoroutine(walk());
                    break;
                case State.Charge:
                    StartCoroutine(charge());
                    break;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        current_hp -= damage;
        if (current_hp < 0 && current_state != State.Dead) {
            current_state = State.Dead;
            StopAllCoroutines();
            StartCoroutine(dead());

            return;
        }

        if(current_state == State.Walk)
            anim.SetTrigger("attacked");
    }

    private IEnumerator walk() {
        int direction = (int)Random.Range(0, 2);
        if (direction == 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        float timer = Time.fixedTime + Random.Range(0, walk_time);
        anim.SetBool("is_walking", true);
        while (timer > Time.fixedTime) {
            transform.Translate(movement_speed * Time.deltaTime, 0, 0);
            yield return null;
        }
        current_state = State.Charge;
        anim.SetBool("is_walking", false);
        can_start_pattern = true;
        yield return null;
    }

    private IEnumerator charge() {
        can_damage = true;
        anim.SetBool("is_charging", true);
        float timer = Time.fixedTime + charge_time;
        int direction = (int)Random.Range(0, 2);
        if (direction == 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        float speed = charge_speed;
        float friction = 0.1f;
        while (timer > Time.fixedTime) {
            transform.Translate(speed * Time.deltaTime, 0, 0);
            if(speed > 0)
                speed -= Time.deltaTime * friction;
            friction += 0.5f;
            yield return null;
        }

        can_start_pattern = true;
        current_state = State.Walk;
        anim.SetBool("is_charging", false);
        can_damage = false;
        yield return null;
    }

    private IEnumerator dead() {
        while (rb.velocity.y != 0) {
            yield return null;
        }
        anim.SetTrigger("die");
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        BoxCollider2D box_collider = this.GetComponent<BoxCollider2D>();
        box_collider.isTrigger = true;
        yield return null;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (current_state == State.Charge && can_damage) {
            player_stats player = collision.transform.GetComponent<player_stats>();
            if (player == null) return;
            can_damage = false;
            player.TakeDamage(attack_damage);
        }
    }

    private void check_mission()
    {
        Missions mission_manager = GameObject.FindGameObjectWithTag("GeneralManager").GetComponent<Missions>();
        if (mission_manager.current_submission == null) return;
        SubMission current_submission = mission_manager.current_submission;
        if (current_submission.kill_mission)
        {
            int index = -1;
            for (int i = 0; i < current_submission.names_to_kill.Length; i++)
            {
                if (current_submission.names_to_kill[i] == this.gameObject.name)
                {
                    index = i;
                }
            }

            if (index == -1) return;

            current_submission.progresses[index]++;
        }
    }
}
