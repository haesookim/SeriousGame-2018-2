using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MissionManager : MonoBehaviour {

    private List<Mission[]> missions = new List<Mission[]>();

    public Mission[] Day1;
    public Mission[] Day2;
    public Mission[] Day3;

    private void Awake()
    {
        missions.Add(Day1);
        missions.Add(Day2);
        missions.Add(Day3);
    }

    private void OnEnable()
    {
        GeneralManager GM = GameObject.FindGameObjectWithTag("GeneralManager").GetComponent<GeneralManager>();
        SetMissionBoard(GM.CurrentDay);
    }

    //Gridmission under MissionBoard
    [SerializeField] private GameObject gridmission;
    private void SetMissionBoard(int day)
    {
        resetMissionBoard(gridmission);

        Mission[] missions_to_set = missions[day-1];
        for(int i = 0; i < missions_to_set.Length; i++)
        {
            Transform mission_tab = gridmission.transform.GetChild(i);
            mission_tab.gameObject.SetActive(true);
            setMissionTab(mission_tab, missions_to_set[i]);
        }
    }

    //Disable all the mission boards
    private void resetMissionBoard(GameObject grid)
    {
        for(int i = 0; i < grid.transform.childCount; i++)
        {
            grid.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    //Set Mission Tab
    private void setMissionTab(Transform mission_tab, Mission mission)
    {
        mission_tab.GetComponent<MissionContainer>().holding_mission = mission;
        //Image mission_image = mission_tab.GetChild(0).GetComponent<Image>();
        Text mission_description = mission_tab.GetChild(1).GetComponent<Text>();
        Text mission_name = mission_tab.GetChild(2).GetComponent<Text>();
        Text mission_rewardDom = mission_tab.GetChild(3).GetComponent<Text>();
        Text mission_rewardMoney = mission_tab.GetChild(4).GetComponent<Text>();
        Text mission_rewardReputation = mission_tab.GetChild(5).GetComponent<Text>();

        //mission_image
        mission_name.text = mission.Mission_Name;
        mission_description.text = mission.Mission_Description_short;
        mission_rewardDom.text = "DOM: " + mission.Reward_DOM;
        mission_rewardMoney.text = "Money: " + mission.Reward_Money;
        mission_rewardReputation.text = "Reputation: " + mission.Reward_Reputation;
    }

}
