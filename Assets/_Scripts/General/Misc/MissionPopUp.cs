using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionPopUp : MonoBehaviour
{
    private Mission player_current_mission;
    GameObject player;
    MissionController mission_controller;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mission_controller = player.GetComponent<MissionController>();
    }
    private void Update()
    {
        player_current_mission = mission_controller.CurrentMission;
        if (player_current_mission != null)
        {
            this.gameObject.SetActive(false);
        }
    }

}
