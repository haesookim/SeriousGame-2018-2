﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mission", menuName = "Mission")]
public class Mission : ScriptableObject {
    public string Mission_Name;
    public float Reward_DOM;
    public float Reward_Reputation;
    public float Reward_Money;

    public SubMission[] submissions;
}
