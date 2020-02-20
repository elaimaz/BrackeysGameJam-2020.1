using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class BasePowerUp : MonoBehaviour
{
    // Set default powerup sound to health, will create custom sounds for each powerup type later if time
    [FMODUnity.EventRef]
    public string FMODEvent = "event:/FX/HealthPowerUp";
    public virtual void OnPowerUpPickedUp(GameObject playerobject) { }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            OnPowerUpPickedUp(collision.gameObject);
            FMOD.Studio.EventInstance PowerUp = FMODUnity.RuntimeManager.CreateInstance(FMODEvent);
            PowerUp.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
            PowerUp.start();
            PowerUp.release();
        }
    }
}
