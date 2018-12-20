using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class V1 : MonoBehaviour, Damageable
{
    [Header("Stat")]
    [SerializeField] private float maximum_hp;
    [SerializeField] private float attack_damage;

    [Header("Attack")]
    [SerializeField] private float punch_time;

    [Header("Walk")]
    [SerializeField] private float walk_time;
    [SerializeField] private float movement_speed;

    [Header("Idle")]
    [SerializeField] private float idle_time;

    //------------------------------[Villain Components]
    private Animator anim;
    private Rigidbody2D rb;

    private float current_hp;

    private enum State
    {
        Walk,
        Punch,
        Idle,
        Dead
    }

    private State current_state = State.Walk;
    private bool can_damage = false;

    private bool can_start_pattern = true;


    void Start()
    {
        //Villain Components
        anim = this.GetComponentInChildren<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
        //Villain Stats
        current_hp = maximum_hp;
        gameObject.GetComponent<Villain_healthbar>().villain_Maximum_Hp = maximum_hp;
    }

    void Update()
    {
        if (can_start_pattern) {
            can_start_pattern = false;
            switch (current_state) {
                case State.Idle:
                    StartCoroutine(idle());
                    break;
                case State.Punch:
                    StartCoroutine(punch());
                    break;
                case State.Walk:
                    StartCoroutine(walk());
                    break;
            }
        }
        gameObject.GetComponent<Villain_healthbar>().villain_current_hp = current_hp;
    }

    public void TakeDamage(float damage)
    {
        current_hp -= damage;
        if (current_hp < 0 && current_state != State.Dead)
        {
            current_state = State.Dead;
            StopAllCoroutines();
            StartCoroutine(dead());
            check_mission();
            return;
        }
        anim.SetTrigger("attacked");

    }

    private IEnumerator walk(){
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

    private IEnumerator idle(){
        float timer = Time.fixedTime + idle_time;
        anim.SetTrigger("idle");
        while (timer > Time.fixedTime) {
            yield return null;
        }
        current_state = next_state();
        can_start_pattern = true;
        yield return null;
    }

    private IEnumerator punch() {
        float timer = Time.fixedTime + punch_time;
        can_damage = true;
        anim.SetTrigger("attack");
        while(timer > Time.fixedTime){
            yield return null;
        }
        can_start_pattern = true;
        current_state = State.Walk;
        can_damage = false;
        yield return null;
    }

    private IEnumerator dead(){
        while (rb.velocity.y!=0)
        {
            yield return null;
        }
        anim.SetTrigger("dead");
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        BoxCollider2D box_collider = this.GetComponent<BoxCollider2D>();
        box_collider.isTrigger = true;
        yield return null;
    }

    private void check_mission() {
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

    private State next_state()
    {
        return (State)Random.Range(0, 3);
    }

}
