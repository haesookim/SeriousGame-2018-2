﻿using System.Collections;
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

    private float current_hp;

    private enum State {
        Walk,
        Charge
    }

    private State current_state = State.Walk;
    private bool can_damage = false;
    
    private bool can_start_pattern = true;

    void Start () {
        //Villain Components
        anim = this.GetComponentInChildren<Animator>();

        //Villain Stats
        current_hp = maximum_hp;
	}
	
	void Update () {
        villain_pattern();
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
        if (current_hp < 0) {
            //KILL VILLAIN
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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (current_state == State.Charge && can_damage) {
            player_stats player = collision.transform.GetComponent<player_stats>();
            if (player == null) return;
            can_damage = false;
            player.TakeDamage(attack_damage);
        }
    }
}
