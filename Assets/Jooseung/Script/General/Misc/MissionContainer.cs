using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MissionContainer : MonoBehaviour {

    public Mission holding_mission;

    public Image missionImage;
    public Text missionDescription;
    public Text missionName;

    private void OnEnable()
    {
        missionDescription.text = holding_mission.Mission_Description;
        missionName.text = holding_mission.Mission_Name;
    }
}
