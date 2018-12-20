using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V4 : MonoBehaviour, Damageable {

    [Header("Stat")]
    [SerializeField] private float maximum_hp;
    [SerializeField] private float attack_damage;

    [Header("Attack")]
    [SerializeField] private float bullet_force;
    [SerializeField] private float bullet_damage;
    [SerializeField] private float bullet_radius;
    [SerializeField] private float bullet_time;

    [Header("Walk")]
    [SerializeField] private float walk_time;
    [SerializeField] private float movement_speed;

    [Header("Idle")]
    [SerializeField] private float idle_time;

    //------------------------------[Villain Components]
    private Animator anim;
    private float current_hp;
    private bool can_start_pattern = true;
    private Rigidbody2D rb;

    [Header("Weapon")]
    public GameObject bullet;
    public Transform bullet_position;

    private enum State
    {
        Walk,
        Shoot,
        Idle,
        Dead
    }

    private State current_state = State.Idle;

    void Start() {
        current_hp = maximum_hp;
        anim = this.GetComponentInChildren<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
        gameObject.GetComponent<Villain_healthbar>().villain_Maximum_Hp = maximum_hp;
    }

    void Update() {

        if (can_start_pattern && current_state != State.Dead) {
            can_start_pattern = false;
            switch (current_state) {
                case State.Idle:
                    StartCoroutine(idle());
                    break;
                case State.Shoot:
                    StartCoroutine(shoot());
                    break;
                case State.Walk:
                    StartCoroutine(walk());
                    break;
            }
        }
        gameObject.GetComponent<Villain_healthbar>().villain_current_hp = current_hp;
    }

    public void TakeDamage(float damage) {
        current_hp -= damage;
        if (current_hp < 0 && current_state != State.Dead)
        {
            current_state = State.Dead;
            StopAllCoroutines();
            StartCoroutine(dead());
            
            return;
        }
        else if (current_state != State.Dead) {
            anim.SetTrigger("attacked");
        }
    }

    private IEnumerator walk()
    {
        int direction = (int)Random.Range(0, 2);
        if (direction == 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        float timer = Time.fixedTime + Random.Range(0, walk_time);
        anim.SetTrigger("walk");
        while (timer > Time.fixedTime)
        {
            transform.Translate(movement_speed * Time.deltaTime, 0, 0);
            yield return null;
        }
        current_state = next_state();
        can_start_pattern = true;
        yield return null;
    }

    private IEnumerator idle() {
        float timer = Time.fixedTime + idle_time;
        anim.SetTrigger("idle");
        while (timer > Time.fixedTime) {
            yield return null;
        }
        current_state = next_state();
        can_start_pattern = true;
        yield return null;
    }

    private IEnumerator shoot() {
        float timer = Time.fixedTime + 0.27f;
        anim.SetTrigger("shoot");

        GameObject spawned_bullet = Instantiate(bullet, bullet_position);
        spawned_bullet.transform.position = bullet_position.transform.position;
        spawned_bullet.GetComponent<Rigidbody2D>().AddForce((bullet_position.transform.right + bullet_position.transform.up * 0.5f) * bullet_force);
        spawned_bullet.GetComponent<V4_bullet>().SetUpBullet(bullet_damage, bullet_time, bullet_radius);
        spawned_bullet.transform.SetParent(null);
        Physics2D.IgnoreCollision(this.GetComponent<BoxCollider2D>(), spawned_bullet.GetComponent<CircleCollider2D>());

        while (timer > Time.fixedTime) {
            yield return null;
        }
        current_state = next_state();
        can_start_pattern = true;
        yield return null;
    }

    private IEnumerator dead() {
        while (rb.velocity.y != 0)
        {
            yield return null;
        }
        anim.SetTrigger("dead");
        current_state = State.Dead;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        BoxCollider2D box_collider = this.GetComponent<BoxCollider2D>();
        box_collider.isTrigger = true;
        check_mission();
        yield return null;
    }

    private State next_state() {
        return (State)Random.Range(0, 3);
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
