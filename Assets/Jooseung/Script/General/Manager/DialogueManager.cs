﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    public static DialogueManager DM;

    private Text dialogue_box;

    private void Awake()
    {
        InitializeDM();
    }

    private void InitializeDM()
    {
        if(DM == null)
        {
            DontDestroyOnLoad(gameObject);
            DM = this;
        }
        else
        {
            if(DM != this)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Start()
    {
        closeDialogue();
        if(dialogue_box == null)
        {
            dialogue_box = this.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        }
    }

    private int current_line = -1;
    public void OpenDialogue(string[] lines)
    {
        this.gameObject.SetActive(true);
        StartCoroutine(showDialogue(lines));
    }

    private void closeDialogue()
    {
        this.gameObject.SetActive(false);
        current_line = -1;
    }

    private IEnumerator showDialogue(string[] lines)
    {
        string[] dialogue = lines;
        while(current_line < dialogue.Length)
        {
            if(current_line != -1)
                dialogue_box.text = dialogue[current_line];
            if (Input.GetKeyDown(KeyCode.C))
            {
                current_line++;
            }
            yield return null;
        }
        closeDialogue();
        yield return null;
    }
}
