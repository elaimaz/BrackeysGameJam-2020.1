using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleScript : MonoBehaviour
{
    
    public float Horizontal = 0;
    public float Vertical = 0;
    
    public Joystick joystick;
    public GameObject joystickParent;
    // Update is called once per frame
    void Update()
    {
        
        
        Vector2 pos = transform.position - joystickParent.transform.position;
        
        print("x = " + pos.x + "  y = " + pos.y);
        if ((pos.x == 0) && (pos.y == 0)){
            Horizontal = 0;
            Vertical = 0;
        }
        else {
            Horizontal = joystick.Horizontal;
            Vertical = joystick.Vertical;
        }
    }
}
