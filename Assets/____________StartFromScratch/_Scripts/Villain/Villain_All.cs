using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villain_All : MonoBehaviour, Damageable {

    [Header("Stat")]
    [SerializeField] private float maximum_hp;

    private float current_hp;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeDamage(float damage) {
        current_hp -= damage;

    }
}
