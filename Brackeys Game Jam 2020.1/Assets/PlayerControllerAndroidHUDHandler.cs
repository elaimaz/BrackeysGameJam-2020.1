using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerAndroidHUDHandler : MonoBehaviour
{
    public PlayerControllerAndroid Pcontroller;
    
    public void SwitchToMelee(){
        Pcontroller.SwitchToMelee();
    }
    
    public void SwitchToRange(){
        Pcontroller.SwitchToRange();
    }
    
    public void MoveRight(){
        Pcontroller.MoveRight();
    }
    
    public void MoveLeft(){
        Pcontroller.MoveLeft();
    }
    
    public void StartJump(){
        Pcontroller.StartJump();
    }
    public void EndJump(){
        Pcontroller.EndJump();
    }
    
    public void Attack(){
        
    }
}
