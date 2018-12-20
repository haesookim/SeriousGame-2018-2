using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mission", menuName = "Mission")]
public class Mission : ScriptableObject {
    public string Mission_Name;
    public string Mission_Area;
    public float Reward_DOM;
    public float Reward_Reputation;
    public float Reward_Money;

    public SubMission[] submissions;

    public bool is_initialized;
    private GameObject NPC;

    public bool is_core_mission;

    public void initialize() {
        if (is_core_mission) {
            is_initialized = true; return;
        }
        NPC = GameObject.Find("n" + this.name);
        NPC.SetActive(false);
    }
    public void initialize_mission() {
        is_initialized = true;
        if (is_core_mission) return;
        NPC.SetActive(true);
        
    }
    public void complete() {
        is_initialized = false;
        if (is_core_mission) return;
        NPC.SetActive(false);
        
    }
}
