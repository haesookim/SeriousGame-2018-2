using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralManager : MonoBehaviour {

    public static GeneralManager GM;
    private void Awake()
    {
        InitializeGM();
    }
    public bool isInitialized = false;
    public void InitializeGM()
    {
        if (GM == null)
        {
            DontDestroyOnLoad(gameObject);
            GM = this;
        }
        else
        {
            if (GM != this)
            {
                Destroy(gameObject);
            }
        }
        isInitialized = true;
    }

    private void Update()
    {
        if (current_mission == null) {
            mission_alarm.SetActive(true);
            return;
        }

        mission_alarm.SetActive(false);
        current_submission = current_mission.submissions[submission_index];

        if (current_submission == null) return;
        //Initialize Submission
        submission_board.SetActive(true);
        if (!is_submission_initialized)
        {
            is_submission_initialized = true;
            current_submission.Initialize_SubMission();
        }
        //Update Submissionboard
        Text submission_title = submission_board.transform.GetChild(0).GetComponent<Text>();
        Text submission_description = submission_board.transform.GetChild(1).GetComponent<Text>();
        submission_title.text = current_submission.submission_name;
        if (current_submission.chat_mission)
        {
            submission_description.text = current_submission.submission_description;
        }
        else if (current_submission.kill_mission)
        {
            submission_description.text = current_submission.submission_description
                + ":  (" + current_submission.killAmount_progress + " / " + current_submission.killAmount + ")";
        }



        //Check Progress of Submission
        current_submission.Check_Progress();
        if (!current_submission.is_complete) return;
        submission_index++;
        //Move to next Submission
        if (submission_index < current_mission.submissions.Length)
        {
            current_submission = current_mission.submissions[submission_index];
            is_submission_initialized = false;
        }
        //TODO: Move to next mission
        else
        {
            current_mission = null;
            submission_index = 0;
            submission_board.SetActive(false);
            is_submission_initialized = false;
        }
    }

    //Day Controller
    private int current_day = 1;
    public int CurrentDay {
        get {
            return current_day;
        }
    }
    public void NextDay() {
        current_day++;
    }

    //Mission Controller
    private Mission current_mission = null;
    public Mission CurrentMission
    {
        get {
            return current_mission;
        }
        set {
            current_mission = value;
        }
    }

    //SubMission
    private SubMission current_submission = null;
    private int submission_index = 0;
    private bool is_submission_initialized = false;
    public bool Submission_Initialized{
        set {
            is_submission_initialized = value;
        }
    }
    public GameObject submission_board;
    private Text submission_title;
    private Text submission_description;
    public SubMission CurrentSubMission
    {
        get
        {
            return current_submission;
        }
    }

    //Stats
    //DOM
    [SerializeField] private float max_dom;
    private float dom = 0;
    public float DOM {
        get {
            return dom;
        }
    }
    public void updateDOM(float value)
    {
        dom += value;
    }

    //Money
    private float money = 0;
    public float Money {
        get {
            return money;
        }
    }
    public void updateMoney(float value)
    {
        money += value;
    }

    //Reputation
    private float reputation = 0;
    public float Reputation {
        get {
            return reputation;
        }
    }
    public void updateReputation(float value)
    {
        reputation += value;
    }

    public GameObject mission_alarm;


    
}
