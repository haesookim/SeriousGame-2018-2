using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine.SceneManagement;

public class mapController : MonoBehaviour {

    private GeneralManager GM;
    public Sprite QuestMark;

    public GameObject area;
    public float oscillationHeight;

    private float startTime = 0;

    private void OnEnable()
    {
        GM = GameObject.FindObjectOfType<GeneralManager>();
        currentMission_area(GM.CurrentMission);
    }

    private void Update()
    {
        if (area == null)
        {
            startTime = Time.fixedTime;
            return;
        }
            

        pointerOnEffect(area, startTime);
        
    }

    private void pointerOnEffect(GameObject area, float startTime)
    {
        float y = oscillationHeight * Mathf.Sin((Time.fixedTime - startTime) * 5);
        area.transform.position = new Vector2(area.transform.position.x, area.transform.position.y + y);
    }

    private void currentMission_area(Mission currentMission)
    {
        if(currentMission == null)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetChild(1).gameObject.SetActive(false);
            }
            return;
        }
        for(int i = 0; i < transform.childCount; i++)
        {
            /*if(transform.GetChild(i).name == currentMission.area.ToString())
            {
                transform.GetChild(i).GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(i).GetChild(1).gameObject.SetActive(false);
            }
            */
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(area.name);
    }
}
