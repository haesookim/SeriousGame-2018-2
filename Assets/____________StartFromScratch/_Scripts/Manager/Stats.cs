using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour {

    

    //------------------------------[DOM]
    [SerializeField] private float maximum_DOM;
    private float current_DOM = 0;
    public Image DOM_Image;

    private void Start()
    {
        DOM_Image.fillAmount = current_DOM / maximum_DOM;
    }

    public void UpdateDOM(float amount) {
        current_DOM += amount;
        DOM_Image.fillAmount = current_DOM / maximum_DOM;
    }
}
