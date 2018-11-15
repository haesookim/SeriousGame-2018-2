using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    GeneralManager GM;

    private void Awake()
    {
        GM = GameObject.FindObjectOfType<GeneralManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            if (GM.CurrentSubMission.name_to_kill == this.tag)
            {
                GM.CurrentSubMission.killAmount_progress++;
            }
            Destroy(this.gameObject);
        }
        
    }
}
