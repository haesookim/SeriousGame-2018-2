using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_weapon : MonoBehaviour {

    [Header("KeyCode")]
    public KeyCode Attack_Key;

    [Header("Attack Power")]
    [SerializeField] private float punch_power;

    private int current_state = 0;
    private bool can_attack = true;

    //------------------------------[player scripts]
    private player_movement player_movement;
    private player_stats player_stats;

    //------------------------------[player component]
    private SpriteRenderer sprite_renderer;
    private Animator anim;

    //------------------------------[layermask]
    private int layermask;

    void Start () {
        //player scripts
        player_movement = this.GetComponent<player_movement>();
        player_stats = this.GetComponent<player_stats>();
        
        //player components
        sprite_renderer = this.GetComponentInChildren<SpriteRenderer>();
        anim = this.GetComponentInChildren<Animator>();

        flame.GetComponent<ParticleSystem>().Stop();

        layermask = ~(LayerMask.GetMask("Player"));
    }

	void Update () {
        change_weapon();

        if (!can_attack) return;

        if (Input.GetKeyDown(Attack_Key)) {
            can_attack = false;
            player_movement.Can_Move = false;
            anim.SetTrigger("attack");
            switch (current_state)
            {
                case 0:
                    StartCoroutine(punch());
                    break;
                case 1:
                    player_movement.Can_Move = true;
                    can_attack = true;
                    if (!is_shooting) {
                        StartCoroutine(gun_aim());
                    }
                    StartCoroutine(gun_shoot());
                    break;
                case 2:
                    can_attack = true;
                    if (anim.GetBool("is_moving")) {
                        player_movement.Can_Move = true;
                    }
                    anim.SetBool("is_flaming", true);
                    flame.GetComponent<ParticleSystem>().Play();
                    break;
                case 3:
                    player_movement.Can_Move = true;
                    StartCoroutine(canon_shoot());
                    break;
            }
        }
        if (Input.GetKeyUp(Attack_Key)) {
            anim.SetBool("is_flaming", false);
            flame.GetComponent<ParticleSystem>().Stop();
            player_movement.Can_Move = true;
        }
        if (current_state == 2) {
            flame_attack();
        }

	}

    private void change_weapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            current_state = 0;
            anim.SetTrigger("to_basic");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            current_state = 1;
            anim.SetTrigger("to_gun");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            current_state = 2;
            anim.SetTrigger("to_flame");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            current_state = 3;
            anim.SetTrigger("to_grenade");
        }

        anim.SetInteger("current_state", current_state);
    }

    private IEnumerator punch() {
        RaycastHit2D[] hits;
        hits = Physics2D.RaycastAll(transform.position + new Vector3(0, 1, 0), transform.right, 2.0f, layermask);
        Debug.DrawRay(transform.position + new Vector3(0, 1, 0), transform.right * 2, Color.red);
        foreach (RaycastHit2D hit in hits) {
            if (hit)
            {
                Damageable obj = hit.transform.GetComponent<Damageable>();
                if (obj != null)
                {
                    obj.TakeDamage(punch_power);
                }
            }
        }

        float timer = Time.fixedTime + 0.17f;
        while (timer > Time.fixedTime) {
            yield return null;
        }
        can_attack = true;
        player_movement.Can_Move = true;
        yield return null;
    }


    //------------------------------[GUN]
    [Header("Gun")]
    public Transform gun_point_idle;
    public Transform gun_point_walk;
    public Transform gun_point_run;
    public GameObject gun_bullet;
    [SerializeField] private float gun_power;
    [SerializeField] private float gun_speed;
    [SerializeField] private float gun_time;
    private Transform get_gun_point(bool is_moving, bool is_running) {
        if (!is_moving) return gun_point_idle;
        if (is_running) return gun_point_run;
        else return gun_point_walk;
    }
    private bool is_shooting = false;
    private IEnumerator gun_aim()
    {
        is_shooting = true;
        anim.SetBool("is_shooting", is_shooting);
        float time = Time.fixedTime + 2;
        while (time > Time.fixedTime)
        {
            yield return null;
        }
        is_shooting = false;
        anim.SetBool("is_shooting", is_shooting);
        yield return null;
    }
    private IEnumerator gun_shoot()
    {
        bool is_running = anim.GetBool("is_running");
        bool is_moving = anim.GetBool("is_moving");

        Transform gun_point = get_gun_point(is_moving, is_running);
        GameObject spawned_bullet = Instantiate(gun_bullet, gun_point);
        spawned_bullet.transform.SetParent(null);
        spawned_bullet.transform.position = gun_point.transform.position;
        gun_bullet bullet = spawned_bullet.GetComponent<gun_bullet>();
        bullet.Set_Bullet(gun_speed, gun_power, get_direction(), gun_time);
        Physics2D.IgnoreCollision(spawned_bullet.GetComponent<CircleCollider2D>(), this.GetComponent<BoxCollider2D>());
        yield return null;
    }

    //------------------------------[FLAME]
    [Header("Flame")]
    public GameObject flame;
    [SerializeField] private float flame_power;
    private void flame_attack() {
        if (anim.GetBool("is_flaming"))
        {
            flame.SetActive(true);
            float distance = 3;
            if (anim.GetBool("is_moving"))
            {
                distance = 3;
            }
            else if (!anim.GetBool("is_moving")){
                distance = 10;
            }
            RaycastHit2D[] hits = Physics2D.RaycastAll(flame.transform.position, flame.transform.forward, distance);

            foreach (RaycastHit2D hit in hits) {
                Damageable obj = hit.transform.GetComponent<Damageable>();
                if (obj == null) continue;
                obj.TakeDamage(flame_power * Time.fixedTime);
            }
            Debug.DrawRay(flame.transform.position, flame.transform.forward * 10, Color.green);
            Debug.DrawRay(flame.transform.position, flame.transform.forward * 3, Color.blue);
        }
        else if (!anim.GetBool("is_flaming")) {
            flame.SetActive(false);
        }
    }

    //------------------------------[Canon]
    [Header("Canon")]
    //public GameObject boundary;
    public GameObject canon_bullet;
    public Transform canon_point_shoot;
    public float canon_power;
    public float canon_shoot_power;
    public float canon_radius;
    private IEnumerator canon_shoot() {
        float timer_before_shoot = Time.fixedTime + 1f;
        anim.SetTrigger("to_grenade");

        GameObject spawned_canon_bullet = Instantiate(canon_bullet);
        Physics2D.IgnoreCollision(this.transform.GetComponent<BoxCollider2D>(), spawned_canon_bullet.GetComponent<CircleCollider2D>());
        //Physics2D.IgnoreCollision(boundary.GetComponent<PolygonCollider2D>(), spawned_canon_bullet.GetComponent<CircleCollider2D>());
        
        spawned_canon_bullet.transform.SetParent(null);
        spawned_canon_bullet.transform.position = canon_point_shoot.position;
        spawned_canon_bullet.GetComponent<canon_missile>().Set_Canon(canon_power, canon_radius);
        
        Rigidbody2D rb_canon = spawned_canon_bullet.GetComponent<Rigidbody2D>();
        rb_canon.isKinematic = true;

        while (timer_before_shoot > Time.fixedTime) {
            spawned_canon_bullet.transform.position = canon_point_shoot.position;
            yield return null;
        }
        rb_canon.isKinematic = false;
        rb_canon.AddForce((transform.right * get_direction() + 0.5f * transform.up) * canon_shoot_power);
        can_attack = true;
        player_movement.Can_Move = true;
        yield return null;
    }

    private int get_direction() {
        if (transform.rotation.y.Equals(180))
        {
            return -1;
        }
        else {
            return 1;
        }
    }
}
