using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_stats : MonoBehaviour {

    public float Player_Maximum_Hp;

    private float player_current_hp;

    public Image Health_Bar;

    // Use this for initialization
    void Start () {
        player_current_hp = Player_Maximum_Hp;
        Health_Bar.fillAmount = player_current_hp / Player_Maximum_Hp;
	}
	
	// Update is called once per frame
	void Update () {
        Health_Bar.fillAmount = player_current_hp / Player_Maximum_Hp;
    }

    public void TakeDamage(float damage)
    {
        player_current_hp -= damage;
        if (player_current_hp < 0) {
            //Dead
        }
    }
}
