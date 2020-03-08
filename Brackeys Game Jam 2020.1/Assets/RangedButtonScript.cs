using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class RangedButtonScript : Button
{
    public DataContainer container;
    
    void Start(){
        container = GetComponent<DataContainer>();
    }
    
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (container == null) container = GetComponent<DataContainer>();
        container.androidHandler.StartFire();
        print("OnPointerDown");
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        container.androidHandler.StopFire();
        print("OnPointerUp");
    }
}
