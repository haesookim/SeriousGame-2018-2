﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour {

    

    //------------------------------[DOM]
    [SerializeField] private float maximum_DOM;
    private float current_DOM = 0;
    public Image DOM_Image;

    //------------------------------[Money]
    private float current_Money = 0;
    public Text money_text;

    //------------------------------[Reputation]
    public Sprite[] Reputation;
    public Image Reputation_Display;
    private float current_reputation = 50;

    private void Start()
    {
        DOM_Image.fillAmount = current_DOM / maximum_DOM;
    }

    private void Update()
    {
        money_text.text = current_Money.ToString();
        set_reputation(current_reputation); 
    }

    public void UpdateDOM(float amount) {
        current_DOM += amount;
        DOM_Image.fillAmount = current_DOM / maximum_DOM;
    }

    private void set_reputation(float reputation) {
        if (reputation < 20)
        {
            Reputation_Display.sprite = Reputation[0];
            return;
        }
        else if (reputation < 40) {
            Reputation_Display.sprite = Reputation[1];
            return;
        }
        else if (reputation < 60)
        {
            Reputation_Display.sprite = Reputation[2];
            return;
        }
        else if (reputation < 80)
        {
            Reputation_Display.sprite = Reputation[3];
            return;
        }
        else if (reputation < 100)
        {
            Reputation_Display.sprite = Reputation[4];
            return;
        }
    }
}
