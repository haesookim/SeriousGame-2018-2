using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mission", menuName = "Mission")]
public class Mission : ScriptableObject {
    public string Mission_Name;
    public string Mission_Description;

    public string[] Mission_Steps;
}
