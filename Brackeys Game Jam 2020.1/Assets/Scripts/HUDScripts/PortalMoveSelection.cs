using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalMoveSelection : MonoBehaviour
{
    
    public PowerUpBar jumPowerUp;
    public PowerUpBar sheildPowerUp;
    public PowerUpBar speedPowerUp;
    
    public int numKey_pressed;
    public bool portal_initialised = false;
    
    public GameObject secondaryPortal;
    
    public bool pressApproved(int numKey){
        if (!portal_initialised){
            portal_initialised = true;
            numKey_pressed = numKey;
            secondaryPortal.SetActive(true);
            return true;
        }
        else {
            //Check if portal was created using the same key.
            if (numKey_pressed == numKey){
                portal_initialised = false;
                secondaryPortal.SetActive(false);
                return false;
            }
            //If portal allready created, but not same character was pressed.
            return false;
        }
    }
}
