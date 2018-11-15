using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

    public GameObject questMark;
    public float oscillationHeight;

    public string[] lines;

    private DialogueManager DM;
    private GeneralManager GM;

    public bool hasQuest = true;

    private void Awake()
    {
        GameObject DontDestroy = GameObject.FindGameObjectWithTag("DontDestroy");
        DM = DialogueManager.DM;
    }
    void Update()
    {
        if (hasQuest)
        {
            questMark.SetActive(true);
            questMark.transform.position = this.transform.position + new Vector3(0, 1f, 0);
            Util.icon_float(questMark, oscillationHeight);
        }
        else
        {
            questMark.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player" && hasQuest)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                DM.OpenDialogue(lines);
                hasQuest = false;
            }
        }
        
        
    }
}
