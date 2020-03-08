using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PowerUpButtonScript : Button
{
    private DataContainer container;
    
    void Start(){
        container = GetComponent<DataContainer>();
    }
    
    public override void OnPointerDown(PointerEventData eventData)
    {
        container.designatedPowerUp.AndroidToogleSelectPowerUp();
        print("OnPointerDown");
    }
//    public override void OnPointerUp(PointerEventData eventData)
//    {
//        container.androidHandler.EndJump();
//        print("OnPointerUp");
//    }
}
