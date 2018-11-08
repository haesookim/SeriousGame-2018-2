using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util{

    public static void icon_float(GameObject obj, float oscillationHeight)
    {
        float y = oscillationHeight * Mathf.Sin((Time.fixedTime) * 5);
        obj.transform.position = new Vector2(obj.transform.position.x, obj.transform.position.y + y);
    }

}
