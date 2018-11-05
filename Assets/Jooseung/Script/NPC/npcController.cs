using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcController : MonoBehaviour {

    private bool hasQuest = true;
    public float oscillationHeight;

    public GameObject questMark;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (hasQuest)
        {
            questMark.SetActive(true);
            questMark.transform.position = this.transform.position + new Vector3(0, 1f,0);
            Util.icon_float(questMark, oscillationHeight);
        }
        else
        {
            questMark.SetActive(false);
        }
	}
}
