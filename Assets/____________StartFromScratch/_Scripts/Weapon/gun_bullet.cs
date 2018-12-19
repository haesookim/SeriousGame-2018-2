using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun_bullet : MonoBehaviour {

    private float speed;
    private float damage;
    private int direction;
    private float bullet_time;

    private bool is_initiated;

    public void Set_Bullet(float bullet_speed, float bullet_damage, int bullet_direction, float bullet_time) {
        speed = bullet_speed;
        damage = bullet_damage;
        direction = bullet_direction;
        this.bullet_time = bullet_time;
        is_initiated = true;
    }

    private void Start()
    {
        StartCoroutine(bullet());
    }

    private IEnumerator bullet() {
        while (!is_initiated) {
            yield return null;
        }
        float time = Time.fixedTime + bullet_time;
        while(time > Time.fixedTime)
        {
            transform.Translate(direction * speed * Time.deltaTime, 0, 0);  
            yield return null;
        }
        Destroy(this.gameObject);
        yield return null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Damageable obj = collision.transform.GetComponent<Damageable>();
        if (obj == null) {
            Destroy(this.gameObject);
            return;
        }

        obj.TakeDamage(damage);
        Destroy(this.gameObject);
        return;
    }
}
