using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float HP;
    public float damage;

    GeneralManager GM;

    private void Awake()
    {
        GM = GameObject.FindObjectOfType<GeneralManager>();
    }

    private void Update()
    {
        if(HP <= 0)
        {
            if (GM.CurrentSubMission.name_to_kill == this.tag)
            {
                GM.CurrentSubMission.killAmount_progress++;
            }
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            collision.transform.GetComponent<heroController>().health -= damage;   
        }
        
    }
}
