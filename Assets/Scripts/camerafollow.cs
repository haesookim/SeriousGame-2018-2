using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerafollow : MonoBehaviour {

    public Transform player;
    public bool init = true;
    public Vector3 originpos;

    void Start()
    {
        originpos = transform.position;
    }

    // Update is called once per frame
    void LateUpdate () {
        if (init){
            if(player.position.x > originpos.x){
                Vector3 newpos = new Vector3(player.position.x, transform.position.y, transform.position.y);
                transform.position = newpos;
                init= !init;
            }
        }
	}
}
