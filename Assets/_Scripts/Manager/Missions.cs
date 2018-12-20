using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Missions : MonoBehaviour {

    private List<Mission[]> Daily_Missions = new List<Mission[]>();

    public Mission[] Day1;

    public GameObject Mission_Select;

    private Mission current_mission;
    public SubMission current_submission;
    private int current_index = 0;

    private void Awake()
    {
        Daily_Missions.Add(Day1);
        Load_Mission(0);
    }

    private bool is_submission_initialized = false;
    private void Update()
    {
        if (current_mission == null) return;
        current_submission = current_mission.submissions[current_index];
        if (!is_submission_initialized) {
            current_submission.Initialize_SubMission();
            is_submission_initialized = true;
            return;
        }
        //Check Progress of Submission
        current_submission.Check_Progress();
        if (!current_submission.is_complete) return;
        current_index++;
        is_submission_initialized = false;
    }

    public void Load_Mission(int day) {
        GameObject Mission_Box_1 = Mission_Select.transform.GetChild(0).gameObject;
        GameObject Mission_Box_2 = Mission_Select.transform.GetChild(1).gameObject;

        set_mission_box(Daily_Missions[day][0], Mission_Box_1);
        set_mission_box(Daily_Missions[day][1], Mission_Box_2);
    }

    private void set_mission_box(Mission mission, GameObject Mission_Box) {
        Text title = Mission_Box.transform.GetChild(0).GetComponent<Text>();
        Text description = Mission_Box.transform.GetChild(1).GetComponent<Text>();

        title.text = mission.Mission_Name;
        //description.text = mission.Mission_Name;
    }

    public void Start_Mission1() {
        current_mission = Daily_Missions[0][0];
    }

    public void Start_Mission2() {
        current_mission = Daily_Missions[0][1];
    }
}
