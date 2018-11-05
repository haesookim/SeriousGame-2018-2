using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hoverState : MonoBehaviour {

    private mapController mapController;
    private Vector2 originalPosition;

    private void Start()
    {
        mapController = this.GetComponentInParent<mapController>();
        originalPosition = this.transform.position;
    }

    //TODO: Fix the teleporting map bug

    //When mouse Enters, set the area in the mapcontroller to this gameobject
    public void onPointerEnter()
    {
        mapController.area = this.gameObject;
    }

    //When mouse pointer exits, set the area to null and the position back to its original position
    public void onPointerExit()
    {
        mapController.area = null;
        this.transform.position = originalPosition;
    }
}
