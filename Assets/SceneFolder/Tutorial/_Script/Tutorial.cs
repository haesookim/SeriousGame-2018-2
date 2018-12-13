using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Util;

public class Tutorial : MonoBehaviour {

    /*Tutorial Script
     * 
     * This script was created in order to assist Tutorial scene environment.
     * 
     * Script will contain information about visual effects and player activity.
     */

    public Image fade_away;
    public float inactive_duration;

    public GeneralManager GM;

    public Mission Tutorial_Mission;
    public GameObject UI;
    

	// Use this for initialization
	void Start () {
        StartCoroutine(Util.UI.fade_away(fade_away,1, 0.5f, true));
        StartCoroutine(initialize_tutorial(inactive_duration));       
	}
	
	// Update is called once per frame
	void Update () {
    }

    private IEnumerator initialize_tutorial(float duration) {
        //Freeze Player
        heroController player_controller = this.GetComponent<heroController>();
        player_controller.State = false;

        float time = Time.fixedTime + duration;
        while (time > Time.fixedTime) {
            yield return null;
        }
        //Unfreeze Player
        player_controller.State = true;

        //Set default tutorial mission
        GM.CurrentMission = Tutorial_Mission;

        UI.SetActive(true);
        yield return null;
    }

}
