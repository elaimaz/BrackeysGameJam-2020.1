using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerAndroidHUDHandler : MonoBehaviour
{
    public PlayerControllerAndroid Pcontroller;
    public PlayerWeaponsAndroid Pweapon;
    
    public void SwitchToMelee(){
        Pcontroller.SwitchToMelee();
        Pweapon.SwitchToMelee();
        print("SwitchToMelee");
    }
    
    public void SwitchToRange(){
        Pcontroller.SwitchToRange();
        Pweapon.SwitchToRange();
        print("SwitchToRange");
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
    
    public void StartFire(){
        Pweapon.StartFire();
    }
    public void StopFire(){
        Pweapon.StopFire();
    }
    public void FireMelee(){
        Pweapon.FireMelee();
    }
}
