using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MissionManager : MonoBehaviour {

    private List<Mission[]> day_mission = new List<Mission[]>();
    private MissionController mission_controller;

    public Mission[] Day1;
    public Mission[] Day2;
    public Mission[] Day3;
    

	// Use this for initialization
	void Start () {
        InitMission();
        mission_controller = GameObject.Find("Player").GetComponent<MissionController>();
	}

    private void InitMission()
    {
        day_mission.Add(Day1);
        day_mission.Add(Day2);
        day_mission.Add(Day3);
    }

    public Mission[] GetMission(int day)
    {
        return day_mission[day];
    }

    public void SetCurrentMission()
    {
        Debug.Log(EventSystem.current.currentSelectedGameObject.transform.parent.name);
    }

}
