using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour {

    public static bool exists = false;
    public static GameObject obj;
    public bool isInitialized;

    private void Awake()
    {
        if (!exists)
        {
            exists = true;
            DontDestroyOnLoad(gameObject);
            obj = this.gameObject;
        }
        else
        {
            if(obj != this.gameObject)
            {
                Destroy(gameObject);
            }
        }
        isInitialized = true;
    }

}
