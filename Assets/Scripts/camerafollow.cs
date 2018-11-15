using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerafollow : MonoBehaviour {

    public Transform player;
    public bool init = true;
    public Vector3 originpos;

    public static GameObject Camera;
    private void Awake()
    {
        if(Camera == null)
        {
            DontDestroyOnLoad(this.gameObject);
            Camera = this.gameObject;
        }
        else
        {
            if(Camera != this.gameObject)
            {
                Destroy(this.gameObject);
            }
        }
    }

    void Start()
    {
        originpos = transform.position;
    }

    // Update is called once per frame
    void LateUpdate () {
        if (init){
            if(player.position.x > originpos.x/2){
                init= !init;
            }
        } else{
            Vector3 newpos = new Vector3(player.position.x, transform.position.y, transform.position.z);
            transform.position = newpos;
        }

        if (player.position.y > originpos.y/2){
            Vector3 newpos = new Vector3(transform.position.x, player.position.y, transform.position.z);
            transform.position = newpos;
        }
	}
}
