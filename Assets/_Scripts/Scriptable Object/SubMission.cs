using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SubMission", menuName = "SubMission")]
public class SubMission : ScriptableObject
{
    public string submission_name;
    public string submission_description;

    public bool chat_mission;
    public string name_to_talk;
    public string[] lines;
    DialogueTrigger DT;

    public bool kill_mission;
    public string name_to_kill;
    public int killAmount;
    public int killAmount_progress;
    public GameObject to_spawn;
    public float minX, maxX;
    public float minY, maxY;

    public bool is_complete = false;


    public void Initialize_SubMission()
    {
        is_complete = false;
        killAmount_progress = 0;
        if (chat_mission)
        {
            GameObject to_talk = GameObject.Find(name_to_talk);
            DT = to_talk.GetComponent<DialogueTrigger>();
            DT.hasQuest = true;
            DT.lines = lines;
        }
        if (kill_mission)
        {
            for(int i = 0; i < killAmount; i++)
            {
                float xPos = Random.Range(minX, maxX);
                float yPos = Random.Range(minY, maxY);
                Instantiate(to_spawn, new Vector2(xPos, yPos), Quaternion.identity);
            }
            
        }
    }
    public void Check_Progress()
    {
        if (chat_mission)
        {
            if(DT == null)
            {
                Debug.LogWarning("Dialogue Trigger Not Detected");
                return;
            }
            if(DT.hasQuest == false)
            {
                is_complete = true;
            }
        }
        if (kill_mission)
        {
            if(killAmount_progress >= killAmount)
            {
                is_complete = true;
            }
        }
    }
}
