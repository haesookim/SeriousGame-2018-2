using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyController : MonoBehaviour {
    //This class controls basic elements like HP and idle movement, basically everything that is accessed outside
    //also everything that has to do with the General Manager?
    public float HP;
    public float movementSpeed;

    public bool inactive = true;

    private Rigidbody2D rb;

    private Animator anim;

    GeneralManager GM;

    private void Awake()
    {
        GM = FindObjectOfType<GeneralManager>();
        anim = GetComponent<Animator>();

        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        if (inactive)
        {
            idleMovement();
        }
        if (HP <= 0)
        {
            death();
        }
    }

    private void death()
    {
        //need to find another way to access besides tags.....?

        /*if (GM.CurrentSubMission.name_to_kill == this.tag)
        {
            GM.CurrentSubMission.killAmount_progress++;
        }*/
        Destroy(this.gameObject);
    }

    float timer = 0;
    private int direction = 1;

    private void idleMovement()
    {
        Vector2 enemyPos = this.transform.position;
        anim.SetBool("walking", true);
        if (direction == 1)
        {
            //move left
            this.transform.Translate(movementSpeed * Time.deltaTime, 0, 0);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            if (moveTimer())
            {
                direction = -1;
            }
        }
        else
        {
            //move right
            this.transform.Translate(movementSpeed * Time.deltaTime, 0, 0);
            transform.rotation = Quaternion.Euler(0, 180f, 0);
            if (moveTimer())
            {
                direction = 1;
            }
        }
    }

    private bool moveTimer()
    {
        if (timer <= 3)
        {
            timer += Time.deltaTime;
            return false;
        }
        else
        {
            timer = 0;
            return true;
        }
    }
}
