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
        
        
        layermask = ~(LayerMask.GetMask("Player"));
    }

	void Update () {
        change_weapon();

        if (!can_attack) return;

        if (Input.GetKeyDown(Attack_Key)) {
            can_attack = false;
            player_movement.Can_Move = false;
            switch (current_state)
            {
                case 0:
                    StartCoroutine(punch());
                    break;
                case 1:
                    break;
                case 2:
                    break;
            }
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
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            current_state = 2;
            anim.SetTrigger("to_grenade");
        }

        anim.SetInteger("current_state", current_state);
    }

    private IEnumerator punch() {
        anim.SetTrigger("attack");

        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position + new Vector3(0, 1, 0), transform.right, 2.0f, layermask);

        if (hit) {
            Damageable obj = hit.transform.GetComponent<Damageable>();
            if (obj != null) {
                obj.TakeDamage(punch_power);
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

    private void gun() {

    }
}
