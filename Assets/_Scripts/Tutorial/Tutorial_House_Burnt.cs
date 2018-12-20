using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Tutorial_House_Burnt : MonoBehaviour {

    public PlayableDirector playable;
    
    private destructable burnt_house;

    private bool played = false;

	// Use this for initialization
	void Start () {
        burnt_house = this.GetComponent<destructable>();
        
	}
	
	// Update is called once per frame
	void Update () {
        if (!played && burnt_house.GetCurrentHealth() < 0) {
            played = true;
            playable.Play();
        }
        if (played && burnt_house.GetCurrentHealth() < 0) {
            if (playable.duration == playable.time) {
                playable.Pause();
                SceneManager.LoadScene("Main");
            }
        }
        
	}
}
