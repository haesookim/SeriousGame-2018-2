using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class DialogueTrigger : MonoBehaviour {

    public GameObject questMark;
    public float oscillationHeight;

    public string[] lines;

    private DialogueManager DM;
    private GeneralManager GM;

    public bool hasQuest = true;

    private void Awake()
    {
        DM = GameObject.Find("Dialogue Manager").GetComponent<DialogueManager>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" && hasQuest)
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                DM.OpenDialogue(lines);
                hasQuest = false;
            }
        }
        
        
    }
}
