using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public Vector3 shootforce;
    public int direction;
    private Rigidbody2D rb;
    public float damage = 40;

    // Update is called once per frame
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        shootforce.x = shootforce.x * direction;
        rb.AddForce(shootforce);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy"){
            Hit(collision);
        }
        else if (collision.collider.tag == "Player"){
            //don't do anything
        }
        else if(collision.collider.tag == "ShootableObject"){
            //explode the thing
        }
        //hit the wall
        else {
            Destroy(gameObject);
        }

    }

    void Hit(Collision2D enemyCollider)
    {
        Debug.Log("It hit!");
        enemyCollider.collider.GetComponent<enemyController>().HP -= 40;
        Destroy(gameObject);
    }
}
