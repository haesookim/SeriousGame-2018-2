using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionController : MonoBehaviour {

    private Mission current_mission = null;
    public Mission CurrentMission {
        get
        {
            return current_mission;
        }
        set
        {
            current_mission = value;
        }
    }

}
