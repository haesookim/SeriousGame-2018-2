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
    public string[] names_to_kill;
    public int[] killAmounts;
    public int[] progresses;
    public GameObject[] to_spawn_objs;
    public float minX, maxX;
    public float minY, maxY;

    public bool is_complete = false;


    public void Initialize_SubMission()
    {
        is_complete = false;

        if (chat_mission)
        {
            GameObject to_talk = GameObject.Find(name_to_talk);
            DT = to_talk.GetComponent<DialogueTrigger>();
            DT.hasQuest = true;
            DT.lines = lines;
        }
        if (kill_mission)
        {
            //Reset Progress
            for (int i = 0; i < progresses.Length; i++)
            {
                progresses[i] = 0;
            }
            for (int i = 0; i < killAmounts.Length; i++) {
                for (int j = 0; j < killAmounts[i]; j++) {
                    float xPos = Random.Range(minX, maxX);
                    float yPos = Random.Range(minY, maxY);
                    GameObject spawned_obj = Instantiate(to_spawn_objs[i], new Vector2(xPos, yPos), Quaternion.identity);
                    spawned_obj.name = to_spawn_objs[i].name;
                }
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
            for (int i = 0; i < progresses.Length; i++) {
                if (progresses[i] < killAmounts[i]) {
                    return;
                }
            }
            is_complete = true;
        }
    }
}
