using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Missions : MonoBehaviour {

    private Days day;
    private Stats stat;

    private List<Mission[]> Daily_Missions = new List<Mission[]>();

    public Mission[] Day1;
    public Mission[] Day2;
    public Mission[] Day3;
    public Mission[] Day4;
    

    public GameObject Mission_Select;

    private Mission current_mission;
    public SubMission current_submission;
    private int current_index = 0;

    private int current_day = 0;
    private int current_time = 0;
    private void Awake()
    {
        day = this.GetComponent<Days>();
        stat = this.GetComponent<Stats>();
        Daily_Missions.Add(Day1);
        Daily_Missions.Add(Day2);
        Daily_Missions.Add(Day3);
        Daily_Missions.Add(Day4);

        for (int i = 0; i < Daily_Missions.Count; i++) {
            for (int j = 0; j < Daily_Missions[i].Length; j++) {
                if (Daily_Missions[i][j] == null) continue;
                Daily_Missions[i][j].initialize();
            }
        }
        Load_Mission(0, 0);
    }

    private bool is_submission_initialized = false;
    private void Update()
    {
        if (current_mission == null) return;

        if (current_index >= current_mission.submissions.Length) {
            current_mission.complete();
            stat.UpdateAll(current_mission.Reward_DOM, current_mission.Reward_Reputation, current_mission.Reward_Money);
            day.Increase_Time();
            current_mission = null;
            current_index = 0;
            return;
        }

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

    public void Load_Mission(int day, int time) {
        GameObject Mission_Box_1 = Mission_Select.transform.GetChild(0).gameObject;
        GameObject Mission_Box_2 = Mission_Select.transform.GetChild(1).gameObject;

        Mission_Box_1.SetActive(true);
        Mission_Box_2.SetActive(true);

        //If it's a core mission, then Start Mission immediately
        if (Daily_Missions[day][0 + time * 2].is_core_mission && !Daily_Missions[day][0 + time * 2].is_initialized) {
            current_mission = Daily_Missions[day][0 + time * 2];
            current_mission.initialize();
            GameObject player = GameObject.Find("Player");
            player_movement player_movement = player.GetComponent<player_movement>();
            switch (current_mission.Mission_Area) {
                case "City":
                    player_movement.move_to_city();
                    break;
                case "Slum":
                    player_movement.move_to_slum();
                    break;
                case "Rich":
                    player_movement.move_to_rich();
                    break;
            }
            return;
        }


        if (Daily_Missions[day][0 + time * 2] != null)
            set_mission_box(Daily_Missions[day][0 + time * 2], Mission_Box_1);
        else
            Mission_Box_1.SetActive(false);
        if (Daily_Missions[day][1 + time * 2] != null)
            set_mission_box(Daily_Missions[day][1 + time * 2], Mission_Box_2);
        else {
            Mission_Box_2.SetActive(false);
        }

        current_day = day;
        current_time = time;
    }

    private void set_mission_box(Mission mission, GameObject Mission_Box) {
        Text title = Mission_Box.transform.GetChild(0).GetComponent<Text>();
        Text description = Mission_Box.transform.GetChild(1).GetComponent<Text>();

        title.text = mission.Mission_Name;
        description.text = "DOM: " + mission.Reward_DOM + " Reputation: " + mission.Reward_Reputation + " Money: " + mission.Reward_Money;
    }

    public void Start_Mission1() {
        current_mission = Daily_Missions[current_day][0 + current_time * 2];
        current_mission.initialize_mission();
    }

    public void Start_Mission2() {
        current_mission = Daily_Missions[current_day][1 + current_time * 2];
        current_mission.initialize_mission();
    }
}
