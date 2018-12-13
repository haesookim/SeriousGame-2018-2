using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destructable : MonoBehaviour, Damageable
{

    [SerializeField] private float maximum_health;
    [SerializeField] private float DOM_amount;
    private float current_health;

    void Start() {
        current_health = maximum_health;
    }

    public void TakeDamage(float damage) {
        current_health -= damage;
        if (current_health < 0) {
            //DO SOMETHING WHEN DESTROYED
            Stats DOM = GameObject.FindGameObjectWithTag("GeneralManager").GetComponent<Stats>();
            DOM.UpdateDOM(DOM_amount);
            Destroy(this.gameObject);
        }
    }
}
