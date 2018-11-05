using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class mapController : MonoBehaviour {

    public GameObject area;
    public float oscillationHeight;

    private float startTime = 0;

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
}
