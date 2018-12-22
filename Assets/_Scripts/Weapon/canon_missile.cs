using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canon_missile : MonoBehaviour {

    private float damage= 40;
    private float radius = 3;

    public void Set_Canon(float damage, float radius) {
        this.damage = damage;
        this.radius = radius;
        this.transform.GetChild(1).GetComponent<ParticleSystem>().Stop();
        Physics2D.IgnoreCollision(this.GetComponent<CircleCollider2D>(), this.GetComponent<CircleCollider2D>());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(explode());
    }

    private IEnumerator explode() {
        this.transform.GetChild(1).GetComponent<ParticleSystem>().Play();
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D hit in hits)
        {
            Debug.Log(hit.name);
            Damageable obj = hit.transform.GetComponent<Damageable>();
            if (obj != null)
            {
                obj.TakeDamage(damage);
            }
        }
        while (this.transform.GetChild(1).GetComponent<ParticleSystem>().isPlaying) {
            yield return null;
        }
        Destroy(this.gameObject);
        yield return null;
    }
}
