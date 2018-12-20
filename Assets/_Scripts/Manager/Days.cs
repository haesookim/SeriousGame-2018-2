using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Days : MonoBehaviour {

    private Stats current_stats;

    private int current_day;
    private int current_time;
	// Use this for initialization
	void Start () {
        current_stats = this.GetComponent<Stats>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
