using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MissionContainer : MonoBehaviour {

    public Mission holding_mission;

    public void SetMission()
    {
        GeneralManager GM = GameObject.FindGameObjectWithTag("GeneralManager").GetComponent<GeneralManager>();
        GM.CurrentMission = holding_mission;
    } 
}
