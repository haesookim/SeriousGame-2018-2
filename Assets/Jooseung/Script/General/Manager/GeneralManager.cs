using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralManager : MonoBehaviour {

    private int current_day = 1;

    public int CurrentDay {
        get {
            return current_day;
        }
    }

    public void NextDay() {
        current_day++;
    }

    
}
