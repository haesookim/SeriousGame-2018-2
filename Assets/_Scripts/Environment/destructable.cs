using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destructable : MonoBehaviour, Damageable
{

    [SerializeField] private float maximum_health;
    [SerializeField] private float DOM_amount;

    public enum Type {
        Rich_Town,
        Slum,
        None
    }

    public enum State {
        Normal,
        Destroyed
    }

    [SerializeField] private Type type;
    private State current_state = State.Normal;

    private float current_health;
    private GameObject before_destroyed;
    private GameObject after_destroyed;

    void Start() {
        current_health = maximum_health;
        before_destroyed = this.transform.GetChild(0).gameObject;
        after_destroyed = this.transform.GetChild(1).gameObject;
    }

    public void TakeDamage(float damage) {
        current_health -= damage;
        if (current_health < 0 && current_state != State.Destroyed) {
            //DO SOMETHING WHEN DESTROYED
            current_state = State.Destroyed;

            if (this.type == Type.Rich_Town)
            {
                StartCoroutine(destroy_building());
            }
            else {
                Stats DOM = GameObject.FindGameObjectWithTag("GeneralManager").GetComponent<Stats>();
                DOM.UpdateDOM(DOM_amount);
                before_destroyed.SetActive(false);
                after_destroyed.SetActive(true);
            }

        }
    }

    //Destroy Rich Town Building
    public GameObject fires;
    private IEnumerator destroy_building() {
        float timer = Time.fixedTime + 5;

        if (fires != null) {
            fires.SetActive(true);
        }

        while (timer > Time.fixedTime) {
            yield return null;
        }

        if (fires != null) {
            fires.SetActive(false);
        }
        before_destroyed.SetActive(false);
        after_destroyed.SetActive(true);
        Stats DOM = GameObject.FindGameObjectWithTag("GeneralManager").GetComponent<Stats>();
        DOM.UpdateDOM(DOM_amount);
        yield return null;
    }

    public float GetCurrentHealth() {
        return current_health;
    }
}
