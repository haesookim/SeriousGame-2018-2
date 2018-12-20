using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class Days : MonoBehaviour {

    public PlayableDirector timeline;
    public SpriteRenderer sky;
    public Sprite[] sky_images;

    public Image newspaper;
    public Sprite[] newspaper_images;

    private Stats current_stats;
    private Missions mission;

    private int current_day = 0;
    private int current_time = 0;


	// Use this for initialization
	void Start () {
        current_stats = this.GetComponent<Stats>();
        mission = this.GetComponent<Missions>();
	}
	
	// Update is called once per frame
	void Update () {
        sky.sprite = sky_images[current_time];
	}

    public void Increase_Time() {
        current_time++;
        
        if (current_time > 3) {
            new_day();
        }

        mission.Load_Mission(current_day, current_time);
    }

    private void new_day() {
        current_time = 0;
        current_day++;
        set_newspaper(current_stats.Get_Reputation());
        timeline.Play();

        //Remove all the corpses
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enenmy");
        foreach (GameObject enemy in enemies) {
            Destroy(enemy);
        }
    }

    private void set_newspaper(float reputation) {
        if (reputation < 20)
        {
            newspaper.sprite = newspaper_images[(current_day - 1) * 3 + 2];
        }
        else if (reputation < 50)
        {
            newspaper.sprite = newspaper_images[(current_day - 1) * 3 + 1];
        }
        else if (reputation >= 50) {
            newspaper.sprite = newspaper_images[(current_day - 1) * 3];
        }
    }
}
