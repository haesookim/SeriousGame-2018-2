using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V4_bullet : MonoBehaviour {

    private float bullet_damage;
    private float time;
    private float radius;

    private ParticleSystem bullet;
    private ParticleSystem bullet_explosion;

    private bool is_initiated = false;

    public void Start()
    {
        bullet = this.transform.GetChild(0).GetComponent<ParticleSystem>();
        bullet_explosion = this.transform.GetChild(1).GetComponent<ParticleSystem>();
        bullet_explosion.Stop();
        StartCoroutine(bullet_coroutine());
    }

    public void SetUpBullet(float bullet_damage, float time, float radius) {
        this.bullet_damage = bullet_damage;
        this.time = time;
        this.radius = radius;
        is_initiated = true;
    }

    private IEnumerator bullet_coroutine() {
        while (!is_initiated) {
            yield return null;
        }

        float timer = Time.fixedTime + time;
        while (timer > Time.fixedTime) {
            yield return null;
        }

        StartCoroutine(explode());

        yield return null;
    }

    private IEnumerator explode() {
        bullet.Stop();
        bullet_explosion.Play();

        Collider2D[] hits;

        hits = Physics2D.OverlapCircleAll(transform.position, radius, LayerMask.GetMask("Player"));

        foreach (Collider2D hit in hits) {
            hit.transform.GetComponent<player_stats>().TakeDamage(bullet_damage);
        }

        while (bullet_explosion.isPlaying) {
            yield return null;
        }
        Destroy(this.gameObject);
        yield return null;
    }


}
