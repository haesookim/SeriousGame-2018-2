using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour {

    //------------------------------[Input Keys]
    [Header("KeyCode Values")]
    public KeyCode Run_Key;
    public KeyCode Jump_Key;

    //------------------------------[Movement]
    [Header("Movement Factors")]
    [SerializeField] private float speed_walk;
    [SerializeField] private float run_multiplier;
    [SerializeField] private float jump_force;

    private float horizontal_movement;
    private float speed_movement;

    private bool can_move = true;
    public bool Can_Move{
        get {
            return can_move;
        }
        set {
            can_move = value;
        }
    }

    //------------------------------[Animator]
    private Animator anim;

    //------------------------------[Sprite Renderer]
    private SpriteRenderer sprite_renderer;

    //------------------------------[Rigidbody2D]
    private Rigidbody2D rb;

    

    void Start () {
        anim = this.GetComponentInChildren<Animator>();
        sprite_renderer = this.GetComponentInChildren<SpriteRenderer>();
        rb = this.GetComponent<Rigidbody2D>();
	}

    private void FixedUpdate()
    {
        transform.Translate(speed_movement, 0, 0);
    }

    void Update () {
        if (!can_move) {
            speed_movement = 0;
            return;
        }
        
        movement();
        jump();
	}

    private void movement()
    {
        horizontal_movement = Input.GetAxisRaw("Horizontal");

        bool is_moving = false;
        if (!horizontal_movement.Equals(0)) {
            is_moving = true;
        }
        anim.SetBool("is_moving", is_moving);

        if (horizontal_movement > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (horizontal_movement < 0) {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            horizontal_movement *= -1;
        }

        speed_movement = horizontal_movement * speed_walk * Time.deltaTime;

        bool is_running = false;
        if (Input.GetKey(Run_Key))
        {
            speed_movement *= run_multiplier;
            is_running = true;
        }
        anim.SetBool("is_running", is_running);
    }

    //TODO: Make it more natural. Don't add jump animation to the game for now
    private void jump() {
        float velocity_y = rb.velocity.y;

        if (velocity_y.Equals(0) && Input.GetKeyDown(Jump_Key))
        {
            rb.AddForce(Vector2.up * jump_force);
        }
    }
}
